# api-proxy-guardicore:  A .Net Framework proxy wrapper for the Guardicore security appliance Centra API

*Please note that this is not intended to be a comprehensive coverage of the Centra API
I only included what I needed when I wrote it.*

### If there are things you need feel free to contribute

Usage:
- All usage is governed through the DataManager class
- DataManager expects a CentraConfiguration object to be injected that contains url, version and credentials for the API

```csharp
var dm = new DataManager(new CentraConfiguration{ username=<your username>, password=<your password>, url=<your url>, version=<your version> });
List<Incident> incidents = dm.GetIncidents(
                begin: begin,
                end: end,
                severities: new IncidentSeverity[]
                { IncidentSeverity.Low, IncidentSeverity.Medium, IncidentSeverity.High },
                tags: tags);
```

*please let me know if you have questions
Jason D Wilson*
