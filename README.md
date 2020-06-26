# lilyabram.co.uk
Blazor WebAssembly portfolio site, using prismic.io headless CMS for content.

[https://lilyabram.co.uk](https://lilyabram.co.uk)

## Hosted on AWS S3 behind Cloudfront

### Github actions used for CI/CD
- dotnet restore, build, publish
- Output copied to S3 bucket
- Cloudfront cache invalidation request sent for all files
