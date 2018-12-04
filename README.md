
Tool to prune NuGet packages from [ProGet](https://inedo.com/proget). Retains configurable amount of the latest packages for each package in the feed.

```
dotnet ProGetNuGetPruner.dll
 Usages for 'prune-packages' (Prune NuGet packages from ProGet, retaining the provided number of the most recent packages)
  prune-packages <progetaddress> <progetusername> <progetpassword> [-f, --feed-name <feedname>] [-p, --packages-to-retain <packagestoretain>] [--prf <packageregex>]

  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    Arguments
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

     progetaddress -> ProGet URI
    progetusername -> ProGet username
    progetpassword -> ProGet user password
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    Flags
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                     [-f, --feed-name <feedname>] -> Feed name. Defaults to 'Default'
    [-p, --packages-to-retain <packagestoretain>] -> Number of latest packages to retain. Defaults to 10
                           [--prf <packageregex>] -> Regex to match packages (name) to be deleted
  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  ```

Remarks: No affiliation with Inedo. Personal tooling to help, automate maintenance tasks.