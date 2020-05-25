# Multi Auth Sample

## Overview

This sample demonstrates a web application that uses 3 different kinds of external authentication simultaneously: AzureAD, AzureAD B2C, and a generic OpenIdConnect provider. The home page contains links to 3 different pages, each of which is protected with a different kind of authentication. Clicking on one of those links will immediately initiate the authentication flow, and will display the corresponding page if authentication is successful.

This sample is basically a proof-of-concept and is not intended to be production-ready (e.g. only support for sign-in is implemented, not sign-out).

## Pre-reqs / setup

For this sample to fully work, you need several things:

1. In Visual Studio, right-click the solution, select `Set Startup Projects`, select Multiple, and set both projects to `Start`.

2. Update `appsettings.json` with the information for an AzureAD tenant, and an AzureAD B2C tenant. See the section below for more details about this setup.

Once that is ready, just hit F5 in Visual Studio. The browser should open at https://localhost:5002, and you can start clicking on the links for each of the protected pages.

## AzureAD and AzureAD B2C setup

You'll need to set up an AzureAD tenant and an AzureAD B2C tenant, and create app registrations there to let this sample authenticate against them. I recommend you read and follow Microsoft's instructions for that, [here](https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/tree/a8ad88255c696ca90c05ed7072de7826ceeb8a51/1-WebApp-OIDC/1-1-MyOrg#step-1-register-the-sample-with-your-azure-ad-tenant) and [here](https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/tree/a8ad88255c696ca90c05ed7072de7826ceeb8a51/1-WebApp-OIDC/1-1-MyOrg#step-1-register-the-sample-with-your-azure-ad-tenant). Whether you use any of the automations in those instructions to create the apps in the directory, or create them by hand, you'll need to update the RedirectUrls to these:

- For AzureAD: `https://localhost:5002/signin-oidc/azuread`
- For AzureAD B2C: `https://localhost:5002/signin-oidc/azureadb2c`

## Generic OpenIdConnect setup

An OpenIdConnect provider (**not** production ready) is included in this solution as a second project. Everything should work out of the box for that scenario.

When redirected to the OpenIdConnect provider's sign-in page, use `alice / alice` as username and password, and click on `Yes, allow` when prompted about WebApp requesting permissions.


