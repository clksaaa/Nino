﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;

namespace Nino.Benchmark
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            // run quickly:)
            Job baseConfig = Job.ShortRun.WithIterationCount(1).WithWarmupCount(1);

            // Add(baseConfig.With(Runtime.Clr).With(Jit.RyuJit).With(Platform.X64));
            this.AddJob(baseConfig.WithRuntime(CoreRuntime.Core50).WithPlatform(Platform.AnyCpu));
            //this.AddJob(baseConfig.WithRuntime(ClrRuntime.Net48).WithPlatform(Platform.AnyCpu));
            //this.AddJob(baseConfig.WithJit(Jit.LegacyJit).WithRuntime(MonoRuntime.Default).WithPlatform(Platform.AnyCpu));
            //this.AddJob(baseConfig.WithJit(Jit.LegacyJit).WithPlatform(Platform.AnyCpu).WithRuntime(ClrRuntime.Net461));

            this.AddExporter(MarkdownExporter.GitHub);
            this.AddExporter(CsvExporter.Default);
            this.AddDiagnoser(MemoryDiagnoser.Default);

            this.AddColumn(new DataSizeColumn());

            this.Orderer = new CustomOrderer();
        }

        public class CustomOrderer : IOrderer
        {
            public bool SeparateLogicalGroups => false;

            public IEnumerable<BenchmarkCase> GetExecutionOrder(ImmutableArray<BenchmarkCase> benchmarksCase)
            {
                return benchmarksCase;
            }

            public string GetHighlightGroupKey(BenchmarkCase benchmarkCase)
            {
                return benchmarkCase.Descriptor.MethodIndex.ToString();
            }

            public string GetLogicalGroupKey(ImmutableArray<BenchmarkCase> allBenchmarksCases, BenchmarkCase benchmarkCase)
            {
                return String.Empty;
            }

            public IEnumerable<IGrouping<string, BenchmarkCase>> GetLogicalGroupOrder(IEnumerable<IGrouping<string, BenchmarkCase>> logicalGroups)
            {
                return logicalGroups;
            }

            public IEnumerable<BenchmarkCase> GetSummaryOrder(ImmutableArray<BenchmarkCase> benchmarksCases, Summary summary)
            {
                return benchmarksCases
                    .OrderBy(x => x.Descriptor.WorkloadMethod.Name);
            }
        }

        public class DataSizeColumn : IColumn
        {
            public string Id => "DataSize";

            public string ColumnName => "DataSize";

            public bool AlwaysShow => true;

            public ColumnCategory Category => ColumnCategory.Custom;

            public int PriorityInCategory => int.MaxValue;

            public bool IsNumeric => true;

            public UnitType UnitType => UnitType.Size;

            public string Legend => string.Empty;

            public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
            {
                return GetValue(summary, benchmarkCase,
                    new SummaryStyle(null, true, SizeUnit.B, TimeUnit.Millisecond));
            }

            public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
            {
                System.Reflection.MethodInfo mi = benchmarkCase.Descriptor.WorkloadMethod;
                if (!mi.Name.Contains("Serialize"))
                {
                    return "-";
                }

                if (mi.DeclaringType != null)
                {
                    var instance = Activator.CreateInstance(mi.DeclaringType);
                    mi.DeclaringType.GetField("Serializer")?.SetValue(instance, benchmarkCase.Parameters[0].Value);
                    mi.DeclaringType.GetMethod("Setup")?.Invoke(instance, null);

                    var bytes = (byte[])mi.Invoke(instance, null)!;
                    var byteSize = bytes.Length;
                    var cultureInfo = summary.GetCultureInfo();
                    if (style.PrintUnitsInContent)
                    {
                        return SizeValue.FromBytes(byteSize).ToString(style.SizeUnit, cultureInfo);
                    }

                    return byteSize.ToString("0.##", cultureInfo);
                }

                return "-";
            }

            public bool IsAvailable(Summary summary)
            {
                return true;
            }

            public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase)
            {
                return false;
            }
        }
    }
}
