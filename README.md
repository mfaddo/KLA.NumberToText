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

