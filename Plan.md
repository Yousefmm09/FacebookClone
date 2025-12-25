 # FacebookClone Backend – Development Plan (Senior Review Roadmap)

## Goal
Take the current backend from “works for demo” to “stable, secure, maintainable, and testable”.

## Targets (choose one)
- **Portfolio-ready**: clean architecture + best practices + some tests.
- **Production MVP**: security, stability, monitoring, CI/CD, migrations.
- **Assignment/demo**: feature completeness + stable demo.

> Current suggested default: **Portfolio-ready** (unless you tell me otherwise).

---

## Workflow (how we work)

### Branching
- `main`: always stable
- `dev`: integration branch (optional)
- Feature branches:
  - `feature/global-exception-handling`
  - `feature/validation`
  - `fix/ef-async-performance`

### Task loop (every task follows this)
1. Create a small task (1–4 hours)
2. Define acceptance criteria (2–5 bullet points)
3. Implement
4. Add/adjust tests (even 1 is OK)
5. Run build + tests
6. Self-review (security + edge cases)
7. Merge

### Definition of Done (DoD)
- Consistent response shape
- No raw exceptions returned to the client
- Validation exists and returns useful 400 responses
- Auth + ownership checks where needed
- Swagger updated (auth + responses)
- At least one test for the change (unit or integration)

---

## Milestones & Backlog

### Milestone A — Stabilize API (highest ROI)
**Goal:** No random 500s, consistent error handling.

**Tasks**
1. Add global exception handling (ProblemDetails)
2. Replace `throw new Exception(...)` with domain exceptions (e.g., NotFound/Forbidden)
3. Standardize status codes:
   - 400 validation
   - 401 unauthorized
   - 403 forbidden
   - 404 not found
   - 409 conflict
4. Make Swagger show common responses and auth

**Acceptance criteria**
- All endpoints return `application/problem+json` on error
- Clients get consistent error fields

---

### Milestone B — Validation & consistency
**Goal:** Enforce input rules centrally.

**Tasks**
1. Add FluentValidation
2. Add MediatR validation pipeline behavior
3. Add paging constraints (max page size, page number min)
4. Standardize response envelopes (especially paged endpoints)

**Acceptance criteria**
- Bad input never reaches services/repositories
- Controllers don’t manually check ModelState everywhere

---

### Milestone C — Security hardening
**Goal:** Remove easy security risks and improve auth flows.

**Tasks**
1. Remove JWT secret from `appsettings.json` (use environment/user-secrets)
2. Fix Swagger environment logic (enable in Development; protect in Production)
3. Add rate limiting to login/OTP endpoints
4. Standardize JWT claim usage (`ClaimTypes.NameIdentifier`)
5. Ensure banned user logic is applied globally (middleware/policy)

**Acceptance criteria**
- No secrets committed to git
- Auth endpoints protected against abuse

---

### Milestone D — Data & EF Core correctness
**Goal:** Fix correctness/performance issues and improve DB integrity.

**Tasks**
1. Fix async EF usage (`CountAsync`, `FirstOrDefaultAsync`, etc.)
2. Remove duplicate `base.OnModelCreating` call
3. Normalize DbSet naming (PascalCase) and avoid shadowing Identity sets
4. Add indexes/constraints:
   - Like uniqueness (UserId + PostId)
   - Friendship uniqueness
5. Add `AsNoTracking()` for read-heavy queries

**Acceptance criteria**
- No sync EF calls inside async methods
- Database enforces critical uniqueness rules

---

### Milestone E — Testing baseline
**Goal:** Prevent regressions, build confidence.

**Minimum viable tests**
1. Unit tests:
   - Auth token creation
   - Post update/delete ownership rules
2. Integration tests:
   - One controller happy-path
   - One validation error path

**Acceptance criteria**
- CI runs tests
- At least 10–20 meaningful tests

---

### Milestone F — Observability & readiness
**Goal:** Easier debugging and health visibility.

**Tasks**
1. Add structured logging (Serilog recommended)
2. Add correlation id / request logging
3. Add Health Checks endpoint (`/health`)
4. Optional: OpenTelemetry tracing

**Acceptance criteria**
- Can trace request → handler → database
- Health endpoint works in deployed environment

---

## Suggested Timeline (3 weeks)

### Week 1 (Stability + Validation)
- Global exception handling (ProblemDetails)
- Domain exceptions + status codes
- FluentValidation + MediatR pipeline
- Refactor controller structure issues (no nested controllers)

### Week 2 (Security + EF fixes)
- Move secrets out of repo
- Swagger environment logic + protection
- Rate limiting for auth/OTP
- EF async + DbContext cleanup + indexes

### Week 3 (Tests + Observability)
- Add test project and core integration tests
- Add logging + correlation id + health checks
- Polish Swagger docs + response models

---

## Task template (copy/paste)

### Title
`<short task name>`

### Scope
API / Core / Service / Infrastructure / Data

### Acceptance criteria
- [ ] ...
- [ ] ...
- [ ] ...

### Notes / risks
- ...

### Test plan
- [ ] ...
