#PasteBin

This is non-official open source version of Pastebin.

## Current Features
- Topic creation with content in s3 (minio)
- Generation of short unique URLs in Redis
- Real-time change of topic content (via websockets - SingalR)
- Storing secrets in vault (dev version)

### Installation
1.  Install Docker
2.  Put self-signed certificate (pfx) in root folder of the project
3. In /deployment folder:
	1. Fill .env file with actual values
	2. Run docker compose up

## TODO
- Different roles in services (pg, minio, redis, vault) - now everywhere admin
- topic ttl everywhere
- what to do if there are no URLs in redis
- move mountPoint(c# code) to env or settings
- use normal vault version (not dev)
- background task -> hangfire?
- metrics
- ef logging
- localize resources
- authentication & authorization
- topic creation
	- dont check user on every edit, check only on join
	- cache most recent & edited topics
- more logs & [logs in elastic](https://learn.microsoft.com/ru-ru/dotnet/core/extensions/logging?tabs=command-line "logs in elastic")