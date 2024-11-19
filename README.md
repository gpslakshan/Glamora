# GLAMORA (E-commerce Application)

![E-Commerce store](https://github.com/gpslakshan/Glamora/blob/main/client/public/thumbnail.png?raw=true)

This project is a full-stack e-commerce application built using the following technologies:

- **Frontend**: Angular, Angular Material, and TailwindCSS
- **Backend**: .NET Core
- **Database**: SQL Server
- **Cache**: Redis for optimized shopping cart functionality
- **Payment Integration**: Stripe for handling secure payments

## Features

- **Product Listings**: Browse products by category, search, and sort.
- **Shopping Cart**: Add, remove, and update cart items (powered by Redis for speed).
- **User Authentication**: Sign up, login, and logout functionality.
- **Stripe Payments**: Secure online payments using Stripe.
- **Order History**: View past orders and order details.
- **Intuitive UI**: An intuitive user interface using Angular Material and TailwindCSS.

## Tech Stack

- **Frontend**: Angular, Angular Material, TailwindCSS
- **Backend**: .NET Core Web API
- **Database**: SQL Server
- **Caching**: Redis (for session-based shopping cart)
- **Payments**: Stripe API

## Architecture

The application follows a layered architecture for maintainability and scalability. The primary components are:

1. **Frontend (Angular)**: Provides a dynamic and responsive user interface.
2. **Backend (.NET)**: A REST API that serves data to the frontend and handles business logic.
3. **Database (SQL Server)**: Manages relational data storage for products, orders, users, etc.
4. **Redis**: Handles session-based caching, particularly for the shopping cart.
5. **Stripe**: Manages secure transactions for payments.

## Pre-Requisites

- **Node.js** (v20 or higher)
- **Angular CLI** (v18 or higher)
- **.NET SDK** (v8 or higher)
- **SQL Server** (Available as a Docker container or installed locally)
- **Redis** (Available as a Docker container or installed locally)
- **Stripe Account** (for payment gateway setup)
