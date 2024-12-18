# Release Notes

The release notes provide a summary of the changes in each version of the Flurl.Http.Spec library. They include information on new features, enhancements, bug fixes, breaking changes, deprecated features, known issues, and the release date.

## vNext (Boron, v0.2.x)

### New Features
- [x] Support multiple requests combined within a single .http file accessed by named keys
- [x] Support dynamic variables in execution context with overwriting predefined static variables

### Enhancements
- [x] Support Fluid template caching (during variable resolution) to improve performance
- [x] New README.md file with FEATURES, RELEASE and CODE_OF_CONDUCT

### Bug Fixes
- [x] [#10](https://github.com/zilch-ai/Flurl.Http.Spec/issues/10), Embedded document links are brokens in NuGet README
- [x] [#12](https://github.com/zilch-ai/Flurl.Http.Spec/issues/12), Exclude Microsoft.CodeAnalysis.Metrics from Nuget Package
- [x] Fix permission issue in labels synchorization workflow.

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
