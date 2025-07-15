### Epic 1: User Onboarding \& Profile Management

#### Feature 1.1: User Registration \& Login



* User Story 1.1.1: As a new user, I want to register with my email and password so I can access the app.



Tasks:



&nbsp;	- Design register API endpoint (POST /api/auth/register)



&nbsp;	- Implement DTO and FluentValidation rules (email format, password strength)



&nbsp;	- Hash password using ASP.NET Core Identity



&nbsp;	- Unit tests for input validation and successful registration



&nbsp;	- UI: Registration form, client-side validation, error display



Acceptance Criteria:



&nbsp;	- Valid email/password creates a new Users record.



&nbsp;	- Duplicate email returns 409 Conflict.



&nbsp;	- Passwords stored as secure hashes.



* **User Story 1.1.2:** As a returning user, I want to log in so I can access personalized features.



Tasks:



&nbsp;	- Implement login API (POST /api/auth/login)



&nbsp;	- Generate JWT with user claims (UserID, Role)



&nbsp;	- Configure YARP gateway to forward token to microservices



&nbsp;	- UI: Login form, error on bad credentials



&nbsp;	- Integration test: valid and invalid credentials



Acceptance Criteria:



&nbsp;	- Successful login returns 200 and a valid JWT.



&nbsp;	- Invalid credentials return 401 Unauthorized.



#### Feature 1.2: Profile Management

* User Story 1.2.1: As a user, I want to view and update my contact information so it stays current.



Tasks:



&nbsp;	- Create GET /api/users/{id} and PUT /api/users/{id}



&nbsp;	- Map FluentResults to handle not-found and validation failures



&nbsp;	- Frontend: Profile page with pre-filled form, save button



&nbsp;	- Unit tests for update scenarios



Acceptance Criteria:



&nbsp;	- User can modify address, phone.



&nbsp;	- Invalid updates (e.g., bad postal code) are rejected with clear messages.



* User Story 1.2.2: As a user, I want to upload identity documents for KYC.



Tasks:



&nbsp;	- Design Azure Blob storage integration for file uploads



&nbsp;	- API endpoint POST /api/users/{id}/documents



&nbsp;	- Store document metadata (DocumentType, URL, UploadedAt)



&nbsp;	- Frontend: drag-and-drop file uploader, progress bar



&nbsp;	- Security: validate file type, virus scan stub



Acceptance Criteria:



&nbsp;	- Uploaded documents save metadata in UserDocuments.



&nbsp;	- UI shows uploaded document list.



### Feature 2.2: Admin Review \& Decision

* **User Story 2.2.1**: As an admin, I want to list pending requests so I can review them.



Tasks:



&nbsp;	- GET /api/admin/loan-requests?status=Pending with pagination



&nbsp;	- Implement YARP route for admin endpoint



&nbsp;	- UI: table with filters (amount, date, user)



&nbsp;	- Non-clustered index on Status for performance



Acceptance Criteria:



&nbsp;	- Admin sees only “Pending” items.



&nbsp;	- Filter and pagination work.



* **User Story 2.2.2**: As an admin, I want to approve or reject requests with feedback.



Tasks:



&nbsp;	- PUT /api/admin/loan-requests/{id}/approve \& /reject



&nbsp;	- Update ReviewedBy, ReviewedAt, Status, Notes



&nbsp;	- On approve → create Loans record (use Polly for transient faults)



&nbsp;	- UI: Approve/Reject buttons, feedback modal



Acceptance Criteria:



&nbsp;	- Approved requests generate a loan.



&nbsp;	- Rejected requests close with Status = “Rejected.”





### Epic 3: Loan Servicing \& Payments

#### Feature 3.1: Amortization Schedule Generation

* **User Story 3.1.1**: As a user, I want to view my payment schedule.



Tasks:



&nbsp;	- On loan creation, compute 36-month amortization with standard formula



&nbsp;	- Insert rows into PaymentSchedule via EF Core bulk insert



&nbsp;	- API GET /api/loans/{id}/schedule



&nbsp;	- UI: calendar-style table with due dates, amounts, status



Acceptance Criteria:



&nbsp;	- Schedule entries match expected Principal/Interest breakdown.



&nbsp;	- Paid vs. Pending statuses display correctly.



#### Feature 3.2: Repayment Processing

* **User Story 3.2.1**: As a user, I want to make a repayment online.



Tasks:



&nbsp;	- Integrate Stripe/PayPal SDK for credit/debit payment



&nbsp;	- POST /api/loans/{id}/payments handles gateway callback



&nbsp;	- Update Transactions and PaymentSchedule (PaidDate, Status)



&nbsp;	- Use Polly retry around payment confirmation



&nbsp;	- UI: payment form, amount validation (min = due amount)



Acceptance Criteria:



&nbsp;	- Successful payment updates ledger and schedule.



&nbsp;	- Failed payments rollback gracefully.



* **User Story 3.2.2**: As a user, I want auto-debit setup for recurring payments.



Tasks:



&nbsp;	- UI: toggle for “Auto Pay,” capture bank account token



&nbsp;	- Hangfire recurring job to execute debit before DueDate



&nbsp;	- Notification on success/failure



Acceptance Criteria:



&nbsp;	- Auto payments run on schedule.



&nbsp;	- Failures notify user and retry according to policy.



### Epic 4: Notifications \& Alerts

#### Feature 4.1: Email \& SMS Notifications

* **User Story 4.1.1:** As a user, I want an email reminder 3 days before each due date.



Tasks:



&nbsp;	- Hangfire scheduled job scans PaymentSchedule for upcoming dues



&nbsp;	- Send templated email via SendGrid API



&nbsp;	- Store notification logs in Notifications table



Acceptance Criteria:



&nbsp;	- Emails send only once per installment.



&nbsp;	- Logs record timestamp and status (Sent/Failed).



* **User Story 4.1.2:** As a user, I want SMS alerts on late payments.



Tasks:



&nbsp;	- Twilio integration for SMS gateway



&nbsp;	- Background job to detect overdue schedules (> DueDate + grace period)



&nbsp;	- UI preference toggle (Opt-in/out)



Acceptance Criteria:



&nbsp;	- Late users receive SMS within 24 hours of overdue.





### Epic 5: Admin Dashboard \& Reporting

#### Feature 5.1: Dashboard Metrics

* **User Story 5.1.1:** As an admin, I want a dashboard of total loans, disbursed amount, delinquency rate.



Tasks:



&nbsp;	- Build SQL views for aggregated metrics



&nbsp;	- API GET /api/admin/dashboard with key KPIs



&nbsp;	- UI: cards and charts (use Chart.js or D3)



Acceptance Criteria:



&nbsp;	- Dashboard loads in <2s with real-time data.



#### Feature 5.2: Export \& Audit Logs

* **User Story 5.2.1:** As an auditor, I want a CSV export of all transactions in a date range.



Tasks:



&nbsp;	- GET /api/admin/transactions/export?from=\&to= streams CSV



&nbsp;	- Audit table records who downloaded and when



Acceptance Criteria:



&nbsp;	- CSV contains correct headers and data.



&nbsp;	- Download action logs user and IP.



### Epic 6: Security, Compliance \& Performance

#### Feature 6.1: Role-Based Access Control

* **User Story 6.1.1**: As an app owner, I want to define roles (User, Admin, Auditor) with specific permissions.



Tasks:



&nbsp;	- Create Roles and UserRoles tables



&nbsp;	- Integrate ASP.NET Core Policies for endpoints



&nbsp;	- UI: admin UI for role assignment



Acceptance Criteria:



&nbsp;	- Unauthorized users receive 403.



&nbsp;	- Admin-only pages hidden from standard users.



#### Feature 6.2: Data Encryption \& Audit

* **User Story 6.2.1**: As a compliance officer, I want PII columns encrypted at rest.



Tasks:



&nbsp;	- Enable Always Encrypted on Users.Email, Users.Phone



&nbsp;	- Configure column master/key in Azure Key Vault



Acceptance Criteria:



&nbsp;	- Data-at-rest encryption verified by DBA.



#### Feature 6.3: Performance Optimization

* **User Story 6.3.1**: As a DevOps engineer, I want caching on frequent reads (loan schedule) to reduce DB load.



Tasks:



&nbsp;	- Integrate Redis cache for PaymentSchedule retrieval



&nbsp;	- Set TTL = 5 minutes, invalidate on payment



Acceptance Criteria:



&nbsp;	- Cache hit ratio ≥ 80% in load tests.

