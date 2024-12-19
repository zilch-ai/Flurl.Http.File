# Release Notes

The release notes provide a summary of the changes in each version of the Flurl.Http.Spec library. They include information on new features, enhancements, bug fixes, breaking changes, deprecated features, known issues, and the release date.

## vNext, v0.2.x (Release Date: TBD)

### New Features
- [ ] Support command variables in execution context: guid, random integer
- [ ] Support environment variables in execution context: system environment variables, .env file

### Enhancements
N/A

### Bug Fixes
N/A

### Breaking Changes
N/A

### Deprecated Features
N/A

### Known Issues
N/A

## v0.2.5 (Release Date: 2024-12-19)

### New Features
- [x] Support multiple requests combined within a single .http file accessed by named keys
- [x] Support dynamic variables in execution context with overwriting predefined static variables

### Enhancements
- [x] Support Fluid template caching (during variable resolution) to improve performance
- [x] New README.md file with FEATURES, RELEASE and CODE_OF_CONDUCT
- [x] Github badges for NuGet (version & downloads), Build Status, Code Coverage, Programming Languages & License
- [x] Github Tag Version Alignment in Nuget Publishing workflow
- [x] Automated Nuget package publishing on tagged release branches creation (with tag version alignment)

### Bug Fixes
- [x] [#10](https://github.com/zilch-ai/Flurl.Http.Spec/issues/10), Embedded document links are brokens in NuGet README
- [x] [#12](https://github.com/zilch-ai/Flurl.Http.Spec/issues/12), Exclude Microsoft.CodeAnalysis.Metrics in Nuget
- [x] [#12](https://github.com/zilch-ai/Flurl.Http.Spec/issues/20), Warnings in Nuget Package Explorer
- [x] Fix 1 permission issue in labels synchorization workflow.

### Breaking Changes
N/A

### Deprecated Features
N/A

### Known Issues
N/A

## v0.1.3 (Release Date: 2024-12-16)

### New Features
- [x] Simplest HTTP GET request parsing & execution with code framing
- [x] Robust & flexible syntax parser for .HTTP files, supports all HTTP methods, headers, and body
- [x] Predefined inline variables (global) as execution context
- [x] Recursive variable references in execution context
- [x] Inline comments in .HTTP files
- [x] HTTP 1.0, 1.1, and 2.0 support

### Enhancements
N/A

### Bug Fixes
N/A

### Breaking Changes
N/A

### Deprecated Features
N/A

### Known Issues
N/A
