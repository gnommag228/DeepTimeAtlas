
# Deep Time Atlas

An interactive map of early human migrations, from roughly 300,000 to 10,000 years ago.

**Status: planning / early development. There is no runnable version yet.**

---

## About

Deep Time Atlas lets you drag a timeline slider and watch archaeological sites and hypothesised migration routes of *Homo sapiens*, Neanderthals and Denisovans appear and fade on a map, with biological and genetic context in an information panel.

It is a personal portfolio project that combines my interest in biology and anthropology with .NET engineering: a relational data model, a REST API, automated tests, CI, and a Blazor client with a custom Leaflet integration.

## Planned features

**MVP**

- Interactive Leaflet map (pan and zoom)
- Timeline slider from -300,000 to -10,000 years with a non-linear scale
- Sites and routes fade in and out according to their date ranges
- Information panel with species, context and sources for each record
- Filtering by species

**Later**

- Paleo-environment layer (land and sea level, ice sheets) synchronised with the timeline
- Ancient DNA samples with a haplogroup filter
- Switching between competing scientific hypotheses

## Tech stack (planned)

| Layer | Technology |
|---|---|
| Client | C# / Blazor WebAssembly, Leaflet.js via a custom JS interop module |
| API | ASP.NET Core Web API, Swagger |
| Data access | Entity Framework Core |
| Database | PostgreSQL or SQL Server (to be decided) |
| Tests | xUnit |
| CI/CD | GitHub Actions |

## Planned architecture

```
DeepTimeAtlas/
├── src/
│   ├── Atlas.Domain/    # entities, enums, DateRange
│   ├── Atlas.Api/       # ASP.NET Core Web API, EF Core, Swagger
│   ├── Atlas.Shared/    # DTOs and contracts shared by API and client
│   └── Atlas.Client/    # Blazor WASM + JS module for Leaflet
├── tests/Atlas.Tests/   # xUnit tests
├── data/                # seed JSON + JSON Schema
├── docker-compose.yml   # local database
└── .github/workflows/   # build, test, deploy
```

**Data model (planned)**

- `Species` 1 — N `Site`
- `Site` N — N `Source`
- `Route` 1 — N `RouteSegment` (ordered points, each with a date range)
- `Route` N — N `Source`
- `DateRange`: from, to, uncertainty; `Confidence` as an enum

## Design decisions

- **DTOs instead of entities** in API responses, so internal structure is not exposed and over-posting is avoided.
- **Non-linear timeline.** Most data sits in the last ~100,000 years, so a linear 290,000-year slider would be unusable.
- **Date ranges, not single dates.** A date like "45,000 ± 3,000" is an interval, and fading is computed from the interval.
- **Batched JS interop.** Data is passed to JavaScript once and filtered by time on the JS side, instead of calling interop on every slider tick.
- **Data as code.** Records are validated against a JSON Schema in CI: no source or no date range means the record is rejected.

## Data and scientific notes

- Migration routes are **scientific hypotheses, not established facts**. Every record is meant to carry a source, a date range and a confidence level.
- Planned data sources include published literature and the Allen Ancient DNA Resource (AADR), a freely available curated collection of ancient human genome-wide data. Ancient DNA coverage for the project's time window is limited, so DNA features come after the core map.
- Dates and routes are simplified for visualisation. Check the cited sources for details.

## Roadmap

- [ ] **Phase 0:** repository, solution, local database, CI skeleton, first 15 sites with sources
- [ ] **Phase 1:** domain model, migrations, API with filtering by species and time, Swagger, unit tests
- [ ] **Phase 2:** map, slider, fading by date range, information panel, deployment of the client
- [ ] **Phase 3:** routes with sources, data validation in CI, hypothesis switcher
- [ ] **Phase 4 (optional):** paleo-environment layer, ancient DNA samples, haplogroup filter

## Getting started

Setup instructions will be added once the first runnable version exists.

## Author

Vladimir Kisarov, Prague. [GitHub](https://github.com/gnommag228) · [LinkedIn](https://www.linkedin.com/in/vladimir-kisarov-a21695373)

## License

To be decided.
