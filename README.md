## Auto Click Bot
This tool provides the opening of a list of URLs contained on a text file automatically.
To implement this feature we use Selenium.Webdriver package available in nuget.org.

### File **Links.txt**
This file contains the list of addressess to open, one adreess by row.

### Output
The result is a html file available on folder **\Results** with the mask 'YYYYMMDDHHMISS.html'.
The information available on this file is the following:

| URL                               | Elapsed time                        |
|-----------------------------------|-------------------------------------|
| The full address opened by the system | Time needeed to complete the action |