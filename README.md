# EnsekWebApplication
Ensek Coding Asssignment

In order to upload Meter Readings, run the application and in Swagger click on the POST method Upload Meter Readings.
Enter 'CSV' in the parameter input box and then click on the upload link. Navigate to the Solutions web directory, and 
select the folder Resources/Uploads. Then select 'Meter_Readings.csv'. The file should upload and trigger the data to
be entered into the database. This can be checked by clicking on the GET method 'Get Meter Readings' which should show the
data for meter readings that has just been entered.

As the brief requires Meter Readings that aren't in then format 'NNNNN' to be filtered out, this prevents most of the readings 
from being added to the database, including zero entries and readings that consist of 1,2,3 and 4 digits. In short, it only
allows readings of 5 digits to be added.
