# Features

Here are the full feature list of the Flurl.Http.Spec library, including all stable/preview features in the latest release, upcoming features in next few releases, and feature items in backlog (as a wish list).
- [x] All features are implemented in latest official release as stable features
- [x] (Preview) All features are implemented in latest official release as preview features
- [ ] (vNext/vBlue) Outcoming features are planned for next few major releases
- [ ] Backlog features are planned for future releases

## Network Transporting

- Transport
  - [x] HTTP/1.0
  - [x] HTTP/1.1
  - [x] HTTP/2.0 
  - [ ] HTTP/3 (QUIC)

- Proxy
  - [ ] HTTP Proxy
  - [ ] SOCKS5 Proxy

- Protocol
  - [x] HTML (Web Pages)
  - [x] JSON (RESTful APIs)
  - [ ] gRPC (over HTTP/2)
  - [ ] ODATA
  - [ ] SOAP
  - [ ] GraphQL

## Specification Format

- Dev Tools Specification
  - .HTTP/.REST file format
    - [x] HTTP Verbs: GET, POST, PUT, PATCH, DELETE
    - [ ] HTTP Verbs: HEAD, OPTIONS, TRACE, CONNECT
    - [x] HTTP Endpoint: Absolute URI with Query Parameters
    - [ ] HTTP Endpoint: Relative Path with Query Parameters
    - [x] HTTP Headers
    - [x] HTTP Request Body
    - [x] Variable Definition & References
    - [x] Inline Comments
    - [x] Request Separators with Identifier
  - [ ] cURL request format
  - [ ] Postman collection file (.postman)

- REST API Specification
  - [ ] OpenAPI specification file (.swagger)
  - [ ] API Blueprint specification file (.apib)
  - [ ] RAML specification file (.raml)

- GraphQL API Specification
  - [ ] GraphQL specification file (.gql)

## Execution Context

- User Variables
  - [x] User variable references in HTTP endpoint, header values & body
  - [x] Recursive variable references: nested variables referencing in variable definitions
  - [ ] Liquid Expression

- System Variables
  - [ ] Dynamic variables
  - [ ] System environment variables
  - [ ] Date/Time expression
  - [ ] GUID generator
  - [ ] Random number generator

- Secret Variables
  - [ ] Docker secrets
  - [ ] Azure Key Vault
  - [ ] User Secrets (in development environment only)

- Workflow Variables
  - [ ] Output Cookie Variables in Api Workflow
  - [ ] Output Json Variables in Api Workflow

## Authentication & Authorization

- Client Auth
  - [ ] API Key
  - [ ] OAuth 2.0 with Client Certificates

- User Auth
  - [ ] Microsoft Identity
  - [ ] OpenID Connect

## Quality & Engineering System

- Documentation
  - [ ] README
  - [ ] Tutorials & Guides
  - [ ] API Reference
  - [ ] Examples & Use Cases
  - [ ] Playgound & Interactive Demo

- Github Integration
  - [x] Github Onboarding: Permission, Boards, Labels & Milestones, Issue Templates, PR Template... 
  - [x] CI/Gated Pipeline via Github Workflow
  - [x] CD/Release Pipeline via Github Workflow
  - [ ] Labeling & Milestone Synchorization Workflow

- Tests
  - [x] Unit Tests Coverage & Report
  - Integration Tests with Project Reference (Pre-Release)
    - [x] HttpBin Tests for Functionality & Compatibility
    - [x] SERP Tests for Real World Scenarios
  - [ ] Integration Tests with Package References (Post-Release)
  - [ ] Smoke Tests before Integration Tests (Pre-Release, Mock Tests for CI/CD Efficiency)

- Build Cops
  - [x] Roslyn Code Analysis
  - [x] Style Cops Analysis
  - [x] xUnit & Moq Analysis
  - [ ] Code Complexity Metrics

- Performance
  - [ ] Benchmarking & Profiling
  - [ ] Stability & Reliability (Multithreading)
  - [ ] Hotspot Analysis & Optimization (Slowness)
  - [ ] Load Testing & Scalability (Bottleneck)
