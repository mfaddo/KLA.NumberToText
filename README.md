# KLA.NumberToText
The Number Converter Application is a tool for converting numeric values into their textual representations. It is primarily designed to convert monetary amounts into words, including dollars and cents.

# Features
<li>Converts numeric values into their textual representations.</li>
<li>Handles both dollars and cents parts separately.</li>
<li>Supports conversion of amounts up to millions.</li>
<li>Validates input numeric format before conversion.</li>

# Installation
## Prerequisites
<li>.NET SDK (version 6.0 or later) installed on your machine.</li>
<br/>
<strong>Clone the Repository</strong>
<br/>

```
https://github.com/mfaddo/KLA.NumberToText.git
```
<strong>Restore NuGet Packages</strong>
```
dotnet restore
```

<strong>Build the Project</strong>
```
dotnet build
```

The application will start and listen for requests at http://localhost:5079


# Usage
## API Endpoints

<li>GET /api/NumbertToText?Convert={number}: convert a number into its textual representation.
</li>

## Example
To get text of this number (999 999 999,99 ), you can make the following request:

```
GET http://localhost:5079/api/NumbertToText?number=999999999,99

```
out put will be

```
nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents
```


# Scalability 
This application is entend for 9 numbers (million) only , however by using the factories I made this app easy to extend so we can extend the app to conver Billion by three ease steps : 

- implement new  `IUnitGetter` type which do the needed logic as below 

```
 public class BillionUnit : IUnitGetter
    {
        public string GetUnit(int indexOfSubset)
        =>
            indexOfSubset switch
            {
                0 => $"{Constants.BILLION} ",
                1 => $"{Constants.MILLION} ",
                2 => $"{Constants.THOUSAND} ",
                3 => string.Empty, 
                _ => throw new
                ArgumentOutOfRangeException(nameof(indexOfSubset))  // Invalid index
            };
    }
```

- add new type to the factory by updating the `UnitFactory` as below

```
public static class UnitFactory
    {
        // Method to create an instance of IUnitGetter based on the number of subsets and subset content
        public static IUnitGetter FactorUnit(int numberOfSubsets, string subset)
        {
            // Check if the subset is empty
            if (subset == Constants.EMPTY_SUBSET)
                return new NoUnit(); // If subset is empty, return NoUnit

            // Switch statement to determine the appropriate unit getter based on the number of subsets
            return numberOfSubsets switch
            {
                1 => new NoUnit(), 
                2 => new ThousandUnit(), 
                3 => new MillionUnit(), 
                4 => new BillionUnit(), // new line
                _ => throw new NotImplementedException("System can not calcuate for more than milion") // Unsupported number of subsets
            };


        }
    }
```
- change the `IsValidForConvert` method we only need to update the regex

**Note**: The third step is required because we are declaring the regex as static in code in real word app we can put this regex in app settings.
