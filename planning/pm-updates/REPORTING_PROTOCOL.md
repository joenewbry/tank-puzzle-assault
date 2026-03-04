# PM Reporting Protocol

**Project:** Tank Puzzle Assault  
**Effective:** 2026-03-03  
**Applies to:** All PM roles (PM Lead, stream PMs, release PM)

---

## 1) Required Status Format (All PM Updates)

Every PM status update must use this structure:

```md
## PM Status Update
- Timestamp: YYYY-MM-DD HH:mm TZ
- PM Owner: <name/agent>
- Task IDs: <e.g., TPA-026, TPA-023>
- Overall State: GREEN | YELLOW | RED
- Progress: <percent or milestone-based progress>

### Completed since last update
- ...

### In progress now
- ...

### Blockers / Risks
- <explicit list, owner, and ETA>
- If none: "None"

### Decisions needed
- <decision, owner, due time>
- If none: "None"

### Next checkpoint
- Time: <next update time>
- Owner: <who reports next>
```

Minimum quality bar:
- Include concrete artifact references where possible (file paths, PR links, commit SHAs).
- Include explicit owner for each blocker.
- Include next checkpoint time (no open-ended updates).

---

## 2) When to Report

PMs must post updates at these triggers:

1. **Time-based cadence (active work):** every **30–45 minutes**.
2. **Milestone complete:** immediately when a milestone/task gate is completed.
3. **Blocker identified:** within **10 minutes** of discovery.
4. **Handoff event:** immediately at handoff between PM/Eng/QA/Release owners.
5. **End-of-shift summary:** before PM goes offline, with current state + next owner.

If no material change since prior update, PM still reports on cadence with:
- “No material change”
- Current blocker state
- Updated ETA / next checkpoint

---

## 3) Where to Write Updates in Repo

## Primary location
- Directory: `planning/pm-updates/`

## File conventions
1. **Daily PM lead snapshot (required):**
   - `planning/pm-updates/pm-lead-status-YYYY-MM-DD.md`
2. **Rolling stream updates (optional but recommended for active gates):**
   - `planning/pm-updates/pm-status-YYYY-MM-DD.md`
   - Append updates in chronological order using the required format.
3. **Protocol/source of truth:**
   - `planning/pm-updates/REPORTING_PROTOCOL.md`

## Cross-linking expectations
- When status affects tracked gate state, PM must also reflect final status in:
  - `planning/workflow.csv` (task status/notes/updated_at)
- When status references implementation evidence, include paths to:
  - `docs/implementation/*`, `docs/qa/*`, `docs/playtest/*`, or PR link records.

---

## 4) Enforcement Notes

- Missing cadence update beyond 45 minutes on critical-path work is treated as a process defect.
- Any RED status must include an escalation owner and recovery ETA.
- PM Lead is accountable for cadence compliance across all PM-owned tracks.
