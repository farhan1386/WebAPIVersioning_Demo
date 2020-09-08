# ASP.Net Web API Versioning

## Why Web API versioning is required?

Once you develop and deploy a Web API service then different clients start consuming your Web API services.

As you know, day by day the business grows and once the business grows then the requirement may change, and once the requirement change then you may need to change the services as well, but the important thing you need to keep in mind is that you need to do the changes to the services in such a way that it should not break any existing client applications who already consuming your services.

This is the ideal scenario when the Web API versioning plays an important role. You need to keep the existing services as it is so that the existing client applications will not break, they worked as it is, and you need to develop a new version of the Web API service which will start consuming by the new client applications.

## What are the Different options available in Web API to maintain the versioning?

1.	URI’s
2.	Query String
3.	Version Header
4.	Accept Header
5.	Media Type
