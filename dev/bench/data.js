window.BENCHMARK_DATA = {
  "lastUpdate": 1664285092317,
  "repoUrl": "https://github.com/sebharskamp/SyntaxParser",
  "entries": {
    "SyntaxParser Benchmark": [
      {
        "commit": {
          "author": {
            "email": "seb.harskamp@gmail.com",
            "name": "sebharskamp",
            "username": "sebharskamp"
          },
          "committer": {
            "email": "seb.harskamp@gmail.com",
            "name": "sebharskamp",
            "username": "sebharskamp"
          },
          "distinct": true,
          "id": "2dde4918f5911fb6d2e33d96508b37f606765682",
          "message": "Updated pipelines\n\nFix benchmarks and CI only on pushes in src directory",
          "timestamp": "2022-09-27T15:11:44+02:00",
          "tree_id": "1db9655d610fc39ee85a13baabaf15af18929a89",
          "url": "https://github.com/sebharskamp/SyntaxParser/commit/2dde4918f5911fb6d2e33d96508b37f606765682"
        },
        "date": 1664284518700,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Benchmark.Benchmarks.ParseText(FilePath: \"./instructions-small\")",
            "value": 711.9749387105306,
            "unit": "ns",
            "range": "± 0.9239774746896192"
          },
          {
            "name": "Benchmark.Benchmarks.ParseText(FilePath: \"./instructions-large\")",
            "value": 209096.93429129463,
            "unit": "ns",
            "range": "± 295.44677735760604"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "seb.harskamp@gmail.com",
            "name": "sebharskamp",
            "username": "sebharskamp"
          },
          "committer": {
            "email": "seb.harskamp@gmail.com",
            "name": "sebharskamp",
            "username": "sebharskamp"
          },
          "distinct": true,
          "id": "a230bc83073576ad01235cb9c1f66b5ce5c21954",
          "message": "Removed newtonsoft dependencies",
          "timestamp": "2022-09-27T15:21:46+02:00",
          "tree_id": "31ea514d5b9153c827f0e79523ff15f4c230a879",
          "url": "https://github.com/sebharskamp/SyntaxParser/commit/a230bc83073576ad01235cb9c1f66b5ce5c21954"
        },
        "date": 1664285085096,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Benchmark.Benchmarks.ParseText(FilePath: \"./instructions-small\")",
            "value": 701.2383974515475,
            "unit": "ns",
            "range": "± 2.3844127587099133"
          },
          {
            "name": "Benchmark.Benchmarks.ParseText(FilePath: \"./instructions-large\")",
            "value": 209359.67320033483,
            "unit": "ns",
            "range": "± 1296.1252595851806"
          }
        ]
      }
    ]
  }
}