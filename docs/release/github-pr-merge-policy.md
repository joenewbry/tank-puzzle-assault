# GitHub PR & Merge Policy

**Project:** Tank Puzzle Assault  
**Owner:** Release PM  
**Task:** TPA-020  
**Last Updated:** 2026-03-03 22:32 PST

---

## 1) Branching Rules

- `main` is the protected release branch.
- All code and documentation changes must land through a Pull Request (PR).
- Direct pushes to `main` are disallowed except for emergency hotfixes explicitly approved by PM + engineering owner.
- Branch naming convention:
  - Feature: `feature/tpa-###-short-topic`
  - Fix/hotfix: `hotfix/tpa-###-short-topic`
  - Docs/process: `docs/tpa-###-short-topic`

## 2) PR Creation Requirements

Each PR must include:

1. Linked task ID in title or body (example: `TPA-026`).
2. Clear summary of scope and non-scope.
3. Test/verification notes (manual or automated).
4. Risk + rollback notes for gameplay-affecting changes.
5. Updated docs when behavior/process changes.

Recommended PR title format:

`[TPA-###] <short imperative summary>`

## 3) Review and Approval Rules

- Minimum approvals before merge: **1**.
- Required reviewer for gameplay/system changes: engineering owner.
- Required reviewer for process/release doc changes: PM/release owner.
- Self-approval is not allowed.
- Unresolved review comments must be addressed or explicitly closed with rationale.

## 4) Merge Gates (Must Pass)

A PR is mergeable only when all are true:

- [ ] PR is linked to a valid workflow task (`planning/workflow.csv`).
- [ ] Required review approvals are present.
- [ ] Requested changes are resolved.
- [ ] No unresolved blocker comments.
- [ ] Merge conflict status is clean.
- [ ] Release-impacting docs are updated (if applicable).

## 5) Merge Method

- Default method: **Squash and merge**.
- Squash commit message format:

`TPA-###: <what changed + why>`

- Rebase merge may be used only when preserving commit history is needed for debugging and is approved by repo owner.

## 6) Post-Merge Actions (Required)

Immediately after merge:

1. Update `planning/pr-links.md` with PR URL.
2. Update `planning/workflow.csv` task status/notes/timestamp.
3. If merge closes a release gate, append a PM checkpoint update in `planning/pm-updates/`.

## 7) Hotfix Exception Path

For P0 production blockers:

1. Open hotfix branch from latest `main`.
2. Create PR with `hotfix` label and explicit rollback note.
3. Require at least one reviewer (not author).
4. Merge after gate checks above are satisfied.
5. Post-merge: trigger targeted QA verification before marking downstream gate complete.

---

This policy is the release process baseline for TPA workflow tasks and should be referenced when executing TPA-023/TPA-026-adjacent merge events.
