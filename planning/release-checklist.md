# Tank Puzzle Assault — Release Checklist

**Owner:** Release PM  
**Related Task:** TPA-020  
**Last Updated:** 2026-03-03 22:32 PST

Use this checklist for every release candidate and final merge-to-main decision.

---

## A) Scope and Readiness

- [ ] Release scope is frozen and mapped to task IDs.
- [ ] All in-scope tasks are in `DONE` state in `planning/workflow.csv`.
- [ ] No open P0/P1 blockers remain for release scope.
- [ ] Known issues list is documented with owner + mitigation.

## B) PR Hygiene

- [ ] Every merged PR is linked in `planning/pr-links.md`.
- [ ] Each PR references its TPA task ID.
- [ ] PR descriptions include verification/testing notes.
- [ ] Merge method followed policy in `docs/release/github-pr-merge-policy.md`.

## C) Validation Evidence

- [ ] QA artifact reviewed (`docs/qa/qa-review.md`).
- [ ] Playtest artifact reviewed (`docs/playtest/playtest-feedback.md`).
- [ ] Any hotfix verification evidence is attached to its task artifact.
- [ ] Platform smoke checks (web/mobile/Xbox target profile) are explicitly noted.

## D) Documentation and Communication

- [ ] Release-impacting docs are up to date.
- [ ] Promotion/comms dependencies are either complete or explicitly deferred with owner.
- [ ] PM status update posted with current release state and next checkpoint.

## E) Final Go/No-Go Gate

- [ ] PM confirms release state: **GO** or **NO-GO**.
- [ ] Decision rationale is recorded in `planning/pm-updates/`.
- [ ] If GO: merge/release timestamp is recorded.
- [ ] If NO-GO: blocker list + recovery ETA is recorded.

---

## Release Sign-off Record

- **Date/Time:** ____________________
- **Decision:** GO / NO-GO
- **PM Owner:** ____________________
- **Notes:** ____________________
