# Tank Puzzle Assault — Core Game Design

## Document Purpose
Define the **fun-first core design** for a Unity puzzle tank game where players solve combat puzzles using **fixed-arc shooting** (no turret tilt), destructible environments, and readable enemy behaviors.

---

## 1) Design Vision

### One-Sentence Fantasy
> “I feel clever and powerful because I can read the battlefield, bounce and arc shells, and break the level itself to win.”

### Design Pillars
1. **Read, then act** — Players should be able to quickly understand what is happening and why a shot worked or failed.
2. **Playful destruction** — Destroying walls, supports, and hazards is not just spectacle; it is the puzzle language.
3. **Simple controls, deep outcomes** — No turret tilt micromanagement; depth comes from position, timing, and target order.
4. **Fast retries, low friction** — Failure should teach in seconds, not punish for minutes.

### Target Session Feel
- Moment-to-moment: 20–60 second “micro-puzzles” inside each encounter.
- Stage-level: 6–10 minute missions with clear escalation.
- Emotional curve: curiosity → confidence → pressure → “aha!” payoff.

---

## 2) Core Gameplay Loop (Fun-First)

### Core Loop
1. **Scan Arena**: identify enemies, destructibles, and chain-reaction opportunities.
2. **Choose Position**: move tank to a lane where fixed-arc shots can clear obstacles or hit weak spots.
3. **Fire & Observe**: launch shell with chosen power band; watch trajectory and impact behavior.
4. **Adapt**: exploit newly opened routes, collapsing structures, and exposed enemy weak points.
5. **Resolve Encounter**: defeat all threats or complete objective (e.g., destroy command node, escort payload).

### Why Fixed Arc (No Tilt) Is Fun
Removing turret tilt reduces control complexity while increasing strategic clarity:
- Players focus on **where to stand** and **when to shoot**.
- Arc mastery comes from repeated readable feedback.
- Puzzle framing stays strong because trajectories are a stable rule set, not a fiddly aiming simulator.

### Combat Puzzle Vocabulary
- **Arc lanes**: low/medium/high obstacle windows.
- **Material rules**: brittle walls crack in 1 hit, reinforced walls need heavy splash, metal reflects/blocks.
- **Chain objects**: explosive barrels, hanging bridges, support columns.
- **Timing objects**: moving doors, patrol windows, conveyor cover blocks.

---

## 3) Shooting & Arc System (No Turret Tilt)

### Input Simplicity
- Horizontal aiming only (turret rotates left/right on plane).
- Arc angle is **fixed per shot profile**; player does not manually tilt barrel.
- Player controls shot distance via **power bands** (tap/short hold/long hold) with strong visual feedback.

### Shot Profiles
Global baseline profile system (all tanks use the same language, stats vary by tank):
- **Quick Shot (tap)**: short range, fast cycle, good for nearby destructibles and interrupts.
- **Standard Shot (short hold)**: default puzzle-solving range.
- **Lob Shot (long hold)**: highest travel arc for “over-cover” solutions.

### Readability Feedback
- Projected landing marker appears on valid terrain while charging.
- Landing marker turns:
  - **White** = neutral hit zone
  - **Yellow** = destructible object in blast radius
  - **Red** = enemy damage zone
- On miss, show a brief ghost arc replay (0.5s) to teach trajectory without extra UI text.

---

## 4) Natural Learning of Arc + Destruction (Teach Through Play)

### Learning Principles
- Teach one interaction at a time.
- Use safe spaces before combat pressure.
- Let players discover with low punishment and immediate reset.

### Early Learning Pattern (Reusable)
1. **See**: place obvious target behind low wall.
2. **Try**: first direct shot fails (wall blocks).
3. **Discover**: a higher-power shot clears wall and lands on target.
4. **Confirm**: second target repeats pattern with slight variation.
5. **Apply under pressure**: one enemy appears using same geometry.

### Destructible Progression
1. **Brittle crate/wall**: “shoot to open path.”
2. **Explosive barrel**: “shoot nearby for area effect.”
3. **Support pillar**: “shoot structure base to collapse catwalk.”
4. **Armor plate + weak point**: “destruction to expose vulnerability.”

### Failure UX
- Failure reason is always visible (“Shell blocked by reinforced wall”).
- One-button restart from checkpoint (<2 seconds).
- Optional hint after two failed attempts: short visual ping on interactive object.

---

## 5) Playable Tanks: Blue vs Green (Simple, Meaningful Differences)

Design goal: both tanks share controls and puzzle language, but encourage different planning styles.

## Blue Tank — “Striker”
**Player fantasy:** precise control, tempo play, safe pokes.

- Lower shell arc and faster shell velocity.
- Faster reload.
- Smaller blast radius.
- Passive trait: **Ricochet Edge** (shots can bounce once off marked metal panels).

**Best at:** precision clears, weak-point sniping, puzzle solutions involving bounce angles.

**Tradeoff:** weaker against clustered enemies and heavy reinforced structures.

## Green Tank — “Breaker”
**Player fantasy:** heavy impact, terrain manipulation, dramatic clears.

- Higher shell arc and slower shell velocity.
- Slower reload.
- Larger blast radius.
- Passive trait: **Demolition Payload** (bonus damage to destructibles and supports).

**Best at:** breaking cover, chain explosions, opening shortcuts through terrain.

**Tradeoff:** lower precision and punishable downtime between shots.

## Co-op/Swap Design Note
In modes with tank switching or co-op, puzzles should support at least:
- one **precision-first** solution (Blue favored), and
- one **demolition-first** solution (Green favored),
so player expression remains valid.

---

## 6) Enemy Design (Red Tanks) + Boss Behavior

All enemy behavior should be readable in under 2 seconds from silhouette + telegraph.

### Red Variant A: Skirmisher
- Fast movement, low HP.
- Prefers flank routes and short peeks.
- Player experience: creates urgency and punishes tunnel vision.

### Red Variant B: Bulwark
- Slow, frontal armor resistant to direct hits.
- Weak rear vents or exposed side joints.
- Player experience: asks for repositioning or environment use (collapse cover, splash behind).

### Red Variant C: Mortar
- Fires high-delay lob shells from backline.
- Clear landing indicator before impact.
- Player experience: area denial pressure that forces movement and shot prioritization.

### Red Variant D: Saboteur
- Deploys temporary barricades/mines.
- Flees after placing tools.
- Player experience: changes puzzle geometry mid-fight, creating dynamic rerouting decisions.

## Boss Tank — “Crimson Bastion” (Player-Experience Framing)

### Encounter Goals
- Feel like a puzzle duel, not a health sponge race.
- Reward pattern recognition, not twitch-only reactions.

### Phase Structure
1. **Phase 1: Fortified Front**
   - Boss frontal shield blocks normal damage.
   - Player must destroy side support towers to remove shield intermittently.
2. **Phase 2: Arena Rewrite**
   - Boss fires shells that create temporary rubble walls.
   - Player must use rubble both as cover and ricochet geometry.
3. **Phase 3: Exposed Core**
   - Weak core opens on overheat windows after failed boss super-shot.
   - Player chooses safe poke (Blue) or burst demolition chain (Green).

### Fairness Rules
- Every major attack has wind-up audio + visual tell.
- No one-shot without clear pre-warning and escape route.
- Checkpoint at phase transitions for reduced frustration.

---

## 7) First 10 Minutes: Onboarding Flow & Tutorial Beats

Goal: player understands movement, fixed-arc shooting, and destructible puzzle logic without heavy text walls.

### Minute 0:00–1:00 — Movement Joy
- Empty playground arena with ramps and breakable props.
- Prompt: “Drive through all beacons.”
- Teaches locomotion, camera, turning comfort.

### Minute 1:00–2:00 — First Shot
- Static target in open line of sight.
- Prompt: “Fire to destroy target.”
- Immediate success establishes weapon feel.

### Minute 2:00–3:30 — Arc Discovery
- Target placed behind low wall.
- Direct tap shot fails visibly.
- Prompt shifts to “Hold fire longer to arc over cover.”
- Second target repeats with different distance for confirmation.

### Minute 3:30–5:00 — Destructible Cause/Effect
- Gate blocked by brittle wall and support pillar nearby.
- Player can shoot wall directly or collapse support for wider opening.
- Teaches multiple valid solutions.

### Minute 5:00–6:30 — Enemy Introduction (Skirmisher)
- One red skirmisher enters with obvious flank route.
- Arena remains simple to avoid cognitive overload.
- Teaches moving while firing and prioritizing threat.

### Minute 6:30–8:00 — Variant Readability
- Bulwark + explosive barrel setup.
- Direct frontal shots ineffective.
- Player discovers splash/flank solution.
- Brief VO/text: “Armor has direction. Use the arena.”

### Minute 8:00–9:00 — Tank Choice Moment
- Safe swap station introduces **Blue vs Green**.
- Micro challenge can be solved by either precision ricochet (Blue) or demolition blast (Green).
- No “wrong” choice; objective is identity understanding.

### Minute 9:00–10:00 — Graduation Encounter
- Short mixed combat-puzzle room with one optional shortcut.
- Success unlocks Mission 1 and settings reminder (“Remap controls anytime”).

### Onboarding Success Criteria
By minute 10, player can:
- Explain difference between tap/hold shot outcomes.
- Identify at least 2 destructible interactions.
- Understand one meaningful difference between Blue and Green.
- Defeat at least 2 enemy variants.

---

## 8) Controls, Settings, Accessibility, Keybind Philosophy

## Control Philosophy
- Default layout must be immediately playable with one hand on movement and one on aim/fire.
- Keep critical actions low-count and consistent across tanks.
- Never gate mastery behind awkward finger gymnastics.

## Default Controls (Keyboard/Mouse)
- Move: **WASD**
- Aim turret: **Mouse X / Right Stick (gamepad)**
- Fire/Charge: **Left Mouse / RT**
- Ability/Utility: **Space / LB**
- Swap tank (if mode allows): **Q / Y**
- Restart checkpoint: **R / View+Y hold**

## Keybind Philosophy
- Full remapping for keyboard + gamepad.
- Support hold/toggle alternatives for charge and aim assist.
- Separate bindings for “Restart Checkpoint” and “Return to Menu” to prevent accidental loss.
- Show currently bound input prompts dynamically in UI.

## Accessibility Requirements (Launch Scope)
1. **Visual**
   - Colorblind-safe palette presets (Deuteranopia/Protanopia/Tritanopia).
   - High-contrast mode for trajectory marker and danger zones.
   - UI scale slider (80–150%).
2. **Motor**
   - Adjustable charge timing windows.
   - Auto-repeat fire option for players unable to hold triggers comfortably.
   - Input buffering and forgiving queue for shot release.
3. **Cognitive**
   - Optional puzzle hint system (off by default in Hard).
   - Simplified HUD toggle (focus mode).
   - Consistent iconography for destructible types.
4. **Audio**
   - Independent sliders: Master / Effects / Voice / Music.
   - Subtitle size + speaker labels.
   - Distinct warning cues for incoming mortar and boss attacks.

## Difficulty Assist Options
- Optional aim landing assist strength (Off/Low/Med).
- Enemy damage multiplier presets.
- Retry aids: additional checkpoint granularity in Story mode.

---

## 9) Difficulty & Progression Guidelines

### Mission Progression Structure
- Mission 1: fundamentals + skirmisher/bulwark.
- Mission 2: timing puzzles + mortar pressure + mixed destructibles.
- Mission 3: advanced chain reactions + saboteur geometry disruption.
- Boss mission: synthesis of all prior verbs.

### Ramp Rules
- Introduce one new element at a time.
- Combine only after player has succeeded twice with each element in isolation.
- Maintain at least one “safe thinking space” per encounter.

---

## 10) UX & Feel Targets (For Prototyping)

- Time-to-restart after fail: **<2.0s**.
- Shot readability: player can predict approximate landing within **3 attempts**.
- Input latency target: **<80ms end-to-end** on target hardware.
- Camera: never fully obstruct tank; dynamic transparency for near occluders.

---

## 11) Out-of-Scope (This Core Doc)

- Detailed economy/progression systems.
- Multiplayer matchmaking/network architecture.
- Final narrative script and lore bible.

---

## 12) Hand-off Notes for Level Design & Engineering

### Level Design
- Build first 3 tutorial rooms exactly around one-mechanic-per-room structure.
- Tag all destructibles with clear VFX families (brittle/explosive/support/reinforced).
- Ensure at least two valid solve paths in mid/late encounters.

### Engineering (Unity)
- Implement deterministic projectile simulation for predictable puzzle behavior.
- Data-drive tank stat deltas (Blue/Green) via ScriptableObjects.
- Expose accessibility values (assist strength, timing windows, UI scale) in central settings system.
- Build fast checkpoint/reset pipeline before content expansion.

---

**Status:** Core design direction approved for prototyping.
