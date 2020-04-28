# About

Example of CQRS pattern REST API.

Used packages: NancyFx, TopShelf, Dapper, Autofac, DI Nancy, Serilog, LibLog

# Run

* app run default on http://127.0.0.1:5000
* you can change host address, for this capability run app with parameter
```cmd
persons.exe -host "http://127.0.0.1:5000"
```
## CreatePerson
|Request|Value|
|---------------|-------|
|Route| http://127.0.0.1:5000/api/v1/person|
| Method |POST|
| Headers|Content-type:application/json|
**Body**
```json
{
    "name": "John",
    "birthDay": "1990-09-09"
}
``` 

### Responses
|Status|Header|Comment|
|-|-|-|
|Ok (200)| Location : /api/v1/persons/{personId} |Correct data|
| Bad Request (400) |-|Unable to deserialize body to Dto|
| Unprocessable Entity (422) |-|Created entity failed validation|



## GetPerson
|Request|Value|
|---------------|-------|
|Route| http://127.0.0.1:5000/api/v1/person/{personId}|
| Method |GET|
### Responses
|Status|Content|Comment|
|-|-|-|
|Ok (200)| JSON PersonDto(see below)|Correct data|
| Not Found (404) |-|Unable to serialize body to Dto|

**PersonDto**
```json
{
    "id": "81435e00-e7b4-4e20-b60c-c6b411969eb7",
    "name": "Test",
    "birthDay": "2002-01-02",
    "age": 18
}
```
