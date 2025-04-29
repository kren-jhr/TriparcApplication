# TriparcApplication

## Getting started

To make testing easier, a Postman collection has been provided [here](https://github.com).

## Design callouts

### Codebase

* I went with a Controller-Service-Repository structure to separate responsibilities and concerns. Controller layer handles validation, service layer handles the business logic, repository layer handles database interactions.

* Injecting dependencies, makes testing easier and reduces each class' overhead.

* DTO for requests, not for response because it maps one-to-one with the domain model. We could add a DTO for response if the situation required (e.g. sensitive data had to be stored on Trips that we don't want to expose).

* Simple data models and classes. Since this project focuses on a specific piece of functionality and making the code simple, I decided against implementing interfaces or adding layers of abstraction. 

* Middleware for error handling and logging. Likewise, I would set up dedicated middleware for HTTP response codes and logging in a more extensive program.

* If this were a complete project, I would set up dedicated secrets management to connect with external services and databases rather than connecting via an open connection string.

### Database choice

I chose MongoDB as the project database for the following reasons (most to least important):

* Iteration speed: Because of its schema flexibility and my previous experience with Mongo, it made development and prototyping faster. *See the Note section for how I would approach such a project in a work context*

* Centralization: Each trip contains all its related information in one document. No need to reference multiple tables to fetch data.

* Schema flexibility: In the event that I had to change my data models over multiple iterations, I could avoid having to spend more time readjusting the schema.

#### Note

For larger-scale applications or future iterations, it would be worth going with a mixed relational-noSQL solution. There are definitely tradeoffs in going with MongoDB/noSQL-only approach.

* No ACID transactions: Especially since we're dealing with cost/prices information, we want to make sure that all transactions are captured successfully.

* Model enforcement: Having a defined schema makes for better planning upfront. Quick iterations are still possible, however, even with the added work of having to write schema update scripts.

* Data guarantees: If we want to guarantee that foreign keys (like ActivityIds) exist upfront, then we should go with relational.

* Relational for structured models where data integrity has to be ensured (cost, contact details, foreign references, etc). NoSQL for more flexible data (activity descriptions, comments, etc).

## Assumptions

* All foreign ids and objects (i.e. ActivityId, UserId, DestinationId) are valid and exist somewhere

* Secrets management is out of scope.

* Keeping authorization out of scope, so no token validation or checking whether userId has the right permissions.

* Activities list can be empty, in which case total cost will be 0.

* Error handling added for the POST flow.