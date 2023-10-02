using System.Drawing;
using RRDNet.Core;
using RRDNet.Graph;
using Xunit;
using System;
using System.Globalization;

namespace RRDNet.Tests;

public class UnitTest2
{

    [Fact]
    public void Should_Display_Graph()
    {
        var now = DateTimeOffset.Parse("2023-10-03 14:00", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

        var step = 5 * 60; // 5min

        RrdDef rrdDef = new("failed-graph.rrd")
        {
            StartTime = now.Add(now.Offset).ToUnixTimeSeconds(),
            Step = step, // 5m
        };

        rrdDef.AddDatasource("alive", "GAUGE", step, 0, Double.NaN);
        rrdDef.AddArchive("AVERAGE", 0.5, 1, 3600 / step * 24 * 30);

        RrdDb rrdDb = new(rrdDef);

        var time = DateTimeOffset.Parse("2023-10-03 14:01", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
        var sample = rrdDb.CreateSample();
        sample.SetAndUpdate($"{time.Add(time.Offset).ToUnixTimeSeconds()}:1");
        rrdDb.Close();

        var from = "2023-10-03 13:59";
        var to = "2023-10-03 14:01";

        var f = DateTimeOffset.Parse(from, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
        var t = DateTimeOffset.Parse(to, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

        RrdGraphDef graphDef = new();

        graphDef.SetTimePeriod(f.Add(f.Offset).ToUnixTimeSeconds(), t.Add(f.Offset).ToUnixTimeSeconds());
        graphDef.Title = $"Pings from: machineId";
        graphDef.Datasource("alive", "failed-graph.rrd", "alive", "AVERAGE");
        graphDef.Area("alive", Color.Green, "ping");
        graphDef.SetValueAxis(1, 1);

        RrdGraph graph = new(graphDef);
        graph.SaveAsPNG("failed-graph.png");
    }
}