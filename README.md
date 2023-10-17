# Url-Shortener 
(by James Joyce Alano)

Technologies used:

1. .NET 5 (MVC basic template)
2. SQLite


Business Logic flow:

1. Upon submitting a valid url - it will be saved to the database and generate an integer value id as primary key
2. Take that generated integer id and convert it to a code via base 62 Bijection
3. Append the generated code to the base url (in this case, the base url for the .NET MVC web application - format of http(s)://baseurl/{code})
4. Modify the database entry for the original url to add the new generated url, plus the generated code from step 2 for easier lookup
5. It will now return back the generated shortened url to the main home page
6. Every visit to the generated url will register (1) hit to the particular original url entry
7. Number of url hits can be seen in the Url Hits page (accessible from shared menu)


Limitations:

1. No security measures implemented
2. Only basic routing rules was implemented (may be wonky for corner-cases)
