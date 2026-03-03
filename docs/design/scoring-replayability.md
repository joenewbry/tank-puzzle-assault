# Tank Puzzle Assault — Scoring & Replayability Design

## Purpose
Create an arcade scoring layer that rewards **clean puzzle solving**, **stylish tank play**, and **repeat attempts** without punishing average players. The system must work for:
- Short sessions (web/mobile)
- Longer mastery sessions (Xbox/controller)
- Friendly competition (solo leaderboard, async rivals, co-op/versus variants)

---

## Design Pillars
1. **Readability first**: players should understand *why* they gained score in under 1 second.
2. **Skill expression without gatekeeping**: efficiency wins, but style and combos can recover a run.
3. **Low frustration loop**: a mistake hurts your ceiling, not your entire run.
4. **Platform-neutral competition**: normalize for input/device differences via puzzle-constrained scoring.

---

## Core Scoring Model
Each stage score is the sum of four lanes:

`StageScore = ClearBase + EfficiencyBonus + StyleBonus + ComboBonus + ObjectiveBonus`

Where:
- **ClearBase**: fixed reward for completion (ensures progress always feels rewarding)
- **EfficiencyBonus**: reward for solving near-or-better than par
- **StyleBonus**: trick shots / tactical flair
- **ComboBonus**: destruction chains and no-idle momentum
- **ObjectiveBonus**: optional goals (no damage, rescue target, etc.)

### 1) ClearBase
- Formula: `ClearBase = 1000 × StageTier`
- StageTier example: 1–5 based on campaign progression.
- Anti-frustration: even poor runs still gain a meaningful baseline.

### 2) EfficiencyBonus (Puzzle Mastery)
Tracks three puzzle KPIs:
- **ShotsUsed** vs **ParShots**
- **MovesUsed** vs **ParMoves**
- **SolveTime** vs **ParTime**

Subscores:
- `ShotEff = clamp(0.6, 1.4, ParShots / max(ShotsUsed,1))`
- `MoveEff = clamp(0.6, 1.4, ParMoves / max(MovesUsed,1))`
- `TimeEff = clamp(0.6, 1.3, ParTime / max(SolveTime,1))`

Weighted efficiency:
- `EffFactor = 0.45*ShotEff + 0.35*MoveEff + 0.20*TimeEff`

Bonus conversion:
- `EfficiencyBonus = round(ClearBase × (EffFactor - 0.75))`
- Typical range: `0` to about `+900` (can be slightly negative in raw calc, but floor at 0 for player-facing score)

**Why this works**:
- Shots and movement matter most to puzzle identity.
- Time matters, but less, avoiding mobile input disadvantage.

### 3) StyleBonus (Arcade Flair)
Style events grant flat points multiplied by current combo tier.

Example style events:
- Ricochet kill (1 bank): +80
- Multi-bank kill (2+ banks): +150
- Long shot (distance threshold): +100
- Thread-the-gap shot: +120
- Environmental tactical kill (barrel/trap): +90
- Last-shell clear (win with final ammo): +200

Style formula:
- `StyleBonus = Σ(EventPoints × ComboTierMult)`
- `ComboTierMult` from combo system below.

### 4) ComboBonus (Destruction Flow)
Combo builds when kills/objective hits occur within a timer.

- Base combo window: 3.0s
- +0.35s extension per kill (max window 5.0s)
- Tier progression:
  - Tier 1: 2 actions → x1.1
  - Tier 2: 4 actions → x1.25
  - Tier 3: 7 actions → x1.5
  - Tier 4: 10+ actions → x1.8

Combo scoring:
- `ComboBonus = Σ((EnemyValue + ChainValue) × (TierMult - 1.0))`

This avoids runaway inflation while rewarding high-skill sequencing.

### 5) ObjectiveBonus
- Optional medals (up to 3 per stage), each +250 to +500.
- Examples: No hull damage, all hostages saved, under par time.
- Encourages replay without requiring perfect play on first clear.

---

## Scoring Formula Examples

## Example A — Solid Clear (mid-skill)
- StageTier 2 → `ClearBase = 2000`
- Par: 8 shots / 14 moves / 120s
- Actual: 9 shots / 15 moves / 130s

Efficiency:
- ShotEff = 8/9 = 0.89
- MoveEff = 14/15 = 0.93
- TimeEff = 120/130 = 0.92
- EffFactor = 0.45(0.89)+0.35(0.93)+0.2(0.92)=0.91
- EfficiencyBonus = 2000×(0.91-0.75)=`+320`

Style + Combo:
- 1 ricochet (+80), 1 long shot (+100), combo mostly Tier1 (+1.1)
- StyleBonus ≈ `(80+100)*1.1 = 198`
- ComboBonus ≈ `+140`

Objectives:
- 1 medal at +250

**StageScore ≈ 2000 + 320 + 198 + 140 + 250 = 2908**

## Example B — Expert Stylish Run
- StageTier 3 → `ClearBase = 3000`
- Actual better than par on shots/moves, near-par time
- EfficiencyBonus = `+980`
- Style events heavy with Tier3/Tier4 multiplier = `+1450`
- ComboBonus = `+620`
- 2 objectives = `+800`

**StageScore ≈ 6850**

The spread is large enough for leaderboard differentiation while a standard clear still feels rewarding.

---

## Anti-Frustration Guardrails
1. **No negative total score**
   - Any negative lane is floored before final sum.
2. **Completion protection**
   - `FinalStageScore >= ClearBase` always.
3. **Combo grace buffer**
   - One short lull (<0.6s beyond timer) consumes a grace charge instead of fully dropping combo once per stage.
4. **Mercy ammo/power-up trigger**
   - If player fails stage 3 times, grant one low-tier tactical consumable on next attempt.
5. **Par bands, not strict single number**
   - Bronze/Silver/Gold par ranges reduce “one move ruined run” frustration.
6. **Input fairness normalization**
   - TimeEff weight capped to 20% so slower touch input is not leaderboard poison.
7. **Soft reset economy**
   - Spent power-ups return partial charge on failed run (e.g., 40%) to prevent hoarding paralysis.

---

## Multiplayer-Compatible Scoring & Friendly Competition

## Modes
1. **Async Leaderboard (primary)**
   - Everyone plays same stage seed/challenge seed.
   - Score calculated identically across platforms.
   - Weekly “division buckets” (Bronze–Diamond) to keep boards aspirational.

2. **Ghost Rival Runs**
   - Replay top path + shot timing as ghost overlay.
   - Player can beat friend ghost in same puzzle seed.

3. **Synchronous Versus (optional)**
   - Shared puzzle board, separate tanks.
   - Personal score + team clear condition.
   - Friendly sabotage disabled in default playlist; enabled only in party mode.

4. **Co-op Score Attack**
   - Team combo meter (shared tier).
   - Assist events score (setup shot, shield rescue).

## Friendly Competition Features
- “Beat Friend by +5%” quick rematch button.
- Personal best delta shown as `+/-` after every stage.
- “Near miss” callout when within 3% of rival score.
- Seasonal badges tied to participation + percentile, not only rank #1.

---

## Power-Up Economy (Risk/Reward)
Power-ups should create tactical choice, not mandatory optimization.

## Currencies
- **Battle Charge (in-run)**: earned by kills, style, objective actions.
- **Supply Tokens (meta)**: earned from clears/challenges; used for loadout unlocks.

## In-Run Economy Rules
- Start each stage with 1 free utility slot (basic smoke or decoy).
- Earn charge milestones at score thresholds (e.g., 1200 / 2600 / 4200).
- Spending charge reduces end-stage “Unspent Charge Bonus,” creating spend-vs-bank tension.

Example:
- Unspent bonus: `+75 × remaining charge pips` (max +375)
- Using a power-up can create larger style/combo gains, so both playstyles are viable.

## Risk/Reward Power-Up Types
- **High risk / high reward**: Overclock Shell (pierce + ricochet, self-recoil hazard)
- **Low risk / low reward**: Smoke Veil (safe reposition, little score upside)
- **Tempo tool**: Magnet Mine (setup for chain kills, high combo potential)
- **Insurance**: Auto-Repair Patch (prevents one failure state, minimal score gain)

Design target: no single power-up should exceed ~18% of total optimal stage score contribution.

---

## Daily / Weekly Challenges (Web + Mobile + Xbox)

## Daily (5–8 minute target)
- **Daily Seed Puzzle**: identical layout globally, 1 attempt + 1 retry token.
- **Style Daily**: score from specific move family only (ricochet day, trap day).
- **Constraint Daily**: low ammo, no power-ups, or fixed loadout.

Rewards:
- Participation reward + streak bonus (capped to avoid burnout).
- Best-of-day score retained; no penalty for early failed attempt.

## Weekly (30–60 minute aggregate)
- **Weekly Circuit**: 5 curated seeds, cumulative score.
- **Weekly Co-op Contract**: optional duo objectives for bonus cosmetics.
- **Boss Puzzle Gauntlet**: escalating modifiers, leaderboard by total points.

Accessibility:
- Challenge windows rotate at local-midnight with a 12-hour grace claim period.
- Controller/touch parity checks on featured seeds before publishing.

---

## Tuning & Telemetry Checklist
Track per stage and per platform:
- Median clear score, P75, P90
- Efficiency lane contribution %
- Style trigger frequency (per attempt)
- Combo drop reasons (timeout, damage, movement idle)
- Power-up pick/use/win-rate deltas
- Retry count before clear

Initial balance targets:
- Typical clear success: 65–80%
- Score lane mix at mid skill: ~55% base/efficiency, ~30% style/combo, ~15% objectives
- Daily challenge completion under 10 minutes for >70% of players

---

## Implementation Notes
- Surface lane breakdown on results screen: `Efficiency / Style / Combo / Objectives`.
- Show live mini-popups for style triggers with concise labels.
- Persist per-seed leaderboard separately from campaign totals.
- Keep formulas data-driven in config tables for rapid seasonal tuning.

This system gives Tank Puzzle Assault a strong arcade “one more run” loop: clear for progress, optimize for mastery, and compete without alienating non-elite players.
