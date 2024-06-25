# Blog Web API
## Project Introduction

**Blog-Web** is an open-source project that provides a comprehensive API for blog applications. The project leverages Entity Framework Core for database management, Identity Framework for user management and authentication, and JWT (JSON Web Token) for authentication and authorization. These technologies collectively offer a complete solution for building secure and feature-rich blog applications.

## Problem Statement

Developing a blog application involves several key challenges:

1. **Database Management:** Efficient and secure storage and retrieval of information.
2. **User Management:** User registration, login, and management.
3. **Authentication and Authorization:** Ensuring valid users can access and perform specific actions within the system.
4. **Content Management:** Effective creation, editing, and deletion of posts and comments.

## Blog-Web Solution

**Blog-Web** addresses these challenges with the following approaches:

1. **Database Management with Entity Framework Core:**
   - Provides an object-oriented approach to database interactions.
   - Simplifies the design, implementation, and maintenance of databases.
   - Supports multiple database management systems, streamlining development.

2. **User Management with Identity Framework:**
   - Offers tools for user registration, login, multi-factor authentication, and role management.
   - Integrates with Entity Framework Core to efficiently store and manage user information.

3. **Authentication and Authorization with JWT:**
   - Uses JSON Web Tokens for user authentication and session management.
   - Facilitates access control based on roles and encrypted information.
   - Reduces server load and improves performance for handling multiple sessions.

4. **Content Management:**
   - Provides APIs for CRUD operations on posts and comments.
   - Enables flexible and effective content management for users.

The project is designed to be easily extensible and customizable, allowing developers to add features and integrate with other services as needed. **Blog-Web** provides a robust foundation for developing customized blog applications tailored to your needs.

## I. Deployment

### 1. Set Up

**Requirements:**
- Visual Studio 2017 or higher
- SQL Server Management Studio (SSMS)
- Postman

**Steps:**
1. Update `appsettings.json` to replace `{your name sql}` with your SSMS instance name.
![image](https://github.com/toiQS/Blog-Web/assets/88361704/199dd5e2-96a7-4c06-965f-dca81a3e2924)


3. Open Package Manager Console in Visual Studio, set **Default Project** to `Blog.Data`.
![image](https://github.com/toiQS/Blog-Web/assets/88361704/2034e11d-9b79-441f-b2f3-472eabe4c8c0)

5. If there are no existing migrations in `Blog.Data`:
    - Use `add-migration {nameDb}` or `entityframeworkcore\add-migration {nameDb}` to convert entities into tables.
    ![image](https://github.com/toiQS/Blog-Web/assets/88361704/3c58a84d-d831-4212-995d-2fa1936cc7dc)

    - Run `update-database` to add tables to your SSMS.
    ![image](https://github.com/toiQS/Blog-Web/assets/88361704/7c19d1ce-f6ea-408a-ac1f-fdd4067d69c3)

6. If migrations already exist or there are no changes in entities, simply run `update-database`.

## II. Run and Testing

### 1. Running the Application

#### Using Swagger
![image](https://github.com/toiQS/Blog-Web/assets/88361704/d6ced3f3-4f77-42df-9b59-089c18fc8e78)


- Press `F5` to start the application. Swagger UI will open in the browser.
- Interact with tasks that do not require authorization, such as *GetAll*.
- For tasks that require authorization (e.g., CRUD operations), obtain authorization first.
![image](https://github.com/toiQS/Blog-Web/assets/88361704/d0f17e6a-5f03-4640-8038-9eca4db419be)


#### Using Postman

- Use Postman for API testing and interaction. Ensure proper authorization where required.
- After logging in through Swagger, obtain the token which is valid for 16 minutes.
- To use the token in Postman:
    1. Copy the token from Swagger.
  ![image](https://github.com/toiQS/Blog-Web/assets/88361704/8184c997-706e-49f3-a240-1f91f435251b)
  ![image](https://github.com/toiQS/Blog-Web/assets/88361704/5798a58b-a49b-4e84-a334-8f57856bfd15)


    3. In Postman, create a new request and enter the API endpoint URL.
  

    5. Go to the **Authorization** tab, select **Bearer Token** in the **Type** dropdown, and paste the token in the **Token** field.
    ![image](https://github.com/toiQS/Blog-Web/assets/88361704/19a525f9-359b-4f76-9950-efa85c892c0b)

    7. Go to the **Body** tab, and select the appropriate body type (e.g., `raw` with JSON) for CRUD operations, if necessary.
    ![image](https://github.com/toiQS/Blog-Web/assets/88361704/192b78cc-3f10-4535-b954-1b8aa96730f7)

    8. Click **Send** to make the request.
    ![image](https://github.com/toiQS/Blog-Web/assets/88361704/94d05bf5-121c-49b2-9572-44ded498709d)

### Libraries and Frameworks

- Entity Framework Core
- Identity Framework
- Jwt Bearer
- Asp.net


#akai
#toiQS
