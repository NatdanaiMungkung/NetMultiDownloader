# NetMultiDownloader Console App

# The project can be run on both Windows and Mac (Should be also run on Linux but didn't try)

# The project written on .Net Core 3.1

# How to run
- Open using Visual Studio (Community 2022)
- Config options can be defined in `NetMultiDownloader/config.json`
- Click run
- Console app will be run and processing queue based on config.json, result will be shown.

# Check list
- The program can accept a single URI or a list of URIs to download. :white_check_mark:
- It should be possible to configure download location as well as number of retries. :white_check_mark: - can be done on `NetMultiDownloader/config.json`
- It should support HTTP/HTTPS, FTP and SFTP. :white_check_mark:
- It should be extensible. Please pay attention to how new protocols can be added. :white_check_mark: (New protocol can implement interface `IDownloader` then add in `AdapterFactory`)
- It should handle retries and partial downloads. If a file fails to fully download then the partial files must be deleted. :white_check_mark:
- It should support parallel downloads. :white_check_mark: (Parallel has been done on `QueueProcessor`)
- It should handle name clashes. If two different resources have the same name, both should download correctly. If the same resource is downloaded twice, it should overwrite the previous one. :white_check_mark: (The filename will be postfix from URI as first 6 characters from MD5 of URI,
so same URI will overwrite, but different uri with same filename will result in different filename
)
- Program architecture is important. :white_check_mark: (Solution architecture has been designed based on described use case with opportunity to extensible to new protocol)
- Don't forget about tests. :white_check_mark: (Unit test project has been included in this solution)