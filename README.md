Under Construction currently

# WebCRM

A cross-platform .Net 5 asp.net MVC application using entity framework core code first SQLite databases and Vue.js.

# Logic Rules
- Accounts have memberships which tie members to accounts.
- Accounts have a primary membership
- Members are tied to users that can self register or be registered.
- Contracts are tied to Members and Accounts.
- A Member can have multiple contracts each of which can be tied a different account.
- Accounts have notes which are not viewable for members.
- Members can give testimonials, which must be approved and can have their viewable text clipped to show only a section of it.
- Contract Transactions should update the total amount paid on the Contract
- Contract Expenses will be tracked for Remittance

# Site Management
- Members will need to be tied to users
- Accounts, members, memberships, contracts, and transactions are all entered manually
- Testimonials given by members will need to be approved before being shown on the main site

# Basic Data Flow
- Data Models of the sql data will implement the ICRMDataModel interface
- ViewModels will implement ICRMViewModel interface referrencing the ICRMDataModel
- Repositories will implement the ICRMRepository interface referrencing the ICRMDataModel and the ICRMViewModel
- An abstract class CRMRepositoryBase will implement basic functionality that repositories can inherit from
- Data will be made available with API controllers
- An abstract class CRMApiControllerBase will implement basic functionality that API controllers can inherit from

# Site Flow
- Vue application with router to pages
- Corresponding vue pages with components
- Javascript calls into the API data controllers and feeds the components

# Security
- DotNet Role Security will be used to authorize controller action and capabilities.
- XSS filter helper applied to string properties on the View Models as they are set from the Models.
- Base API controller will implement checks for each type and provide an ability to implement a restricted selection.
- All database calls are through entity framework and without constructed SQL text.
- All accounts will have to verify emails


