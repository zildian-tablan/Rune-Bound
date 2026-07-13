# RuneBound

RuneBound is a Unity action-adventure game where the player selects a hero class, navigates platforming levels, and defeats enemies using class-specific skills.

## Game overview

RuneBound combines melee combat, ranged attacks, and special skill usage with class-based progression. Players move through levels, avoid hazards, and battle both standard enemies and bosses.

## About this project

- **Created by:** Zildian Bendict Tablan and Vincent Aaron B. Vicete
- **Purpose:** Build a playable Unity action-adventure game prototype for an academic course requirement while demonstrating class selection, skill mechanics, and enemy encounters.
- **Academic context:** Developed as part of a school or university assignment to show Unity gameplay systems, scene design, and project documentation.
- **Why it was made:** To develop and showcase Unity gameplay systems such as character selection, skill UI, damage handling, invincibility states, and boss fight progression.
- **Key goals:** make class-specific abilities feel distinct, support level-based progression, and create reusable UI logic for skills.

> Play online: https://zil30.itch.io/rune-bound

### Available classes

- **Knight**
  - Strong melee combat.
  - Damage boost skill that temporarily increases attack power.
  - Invincibility skill for short periods of immunity.
- **Mage**
  - Ranged fireball attacks.
  - Invincibility skill for surviving dangerous situations.


### Gameplay mechanics

- Skills are activated with dedicated buttons or keys, and cooldowns are shown with UI fill images.
- Damage and invincibility are handled by separate systems for characters and hazards.
- Boss fights include special healthbars,　behavior, and level completion triggers.

## Current features

- Class selection menu with a persistent `PlayerManager` singleton.
- Spawn logic for chosen character in level scenes.
- Skill UI manager that enables/disables skill buttons based on the active class.
- Knight damage boost skill with temporary increased damage and visual feedback.
- Mage fireball attack ability and ranged combat support.
- Status effects including invincibility and instant damage handling.


## Project structure

- `Assets/`
  - `Script/` - Game script logic, including player control, enemies, UI, and level management.
  - `Menu/` - Menu screens, level selectors, player selection, and transition handling.
  - `EnemyScript/` - Enemy behaviors, hazard interactions, and boss logic.
  - `ProjectSettings/` - Unity project settings are stored here.
- `Packages/`
  - `manifest.json` - Defines Unity package dependencies for the project.
- `.gitignore` - Repository ignore rules for Unity-generated files.

## How to open the project

1. Clone the repository to your local machine.
2. Open Unity Hub.
3. Choose `Add` and select the project folder, or open the `RuneBound.slnx` solution if your editor supports it.
4. Allow Unity to import assets and restore packages from `Packages/manifest.json`.

## Development notes

- Use Unity 2023 or newer for compatibility with the Universal Render Pipeline and input system packages listed in `Packages/manifest.json`.
- Avoid committing generated folders such as:
  - `Library/`
  - `Temp/`
  - `obj/`
  - `Logs/`
  - `UserSettings/`
- If you add new assets, make sure `.meta` files are tracked in Git.
- The project uses `com.unity.render-pipelines.universal`, `com.unity.inputsystem`, and `com.unity.cinemachine`.

## Future enhancements

- Add more detailed skill behavior and UI for Archer and Assassin classes.
- Add sound and visual effects for acid, instant-death hazards, and class-specific abilities.
- Improve level design and boss AI behavior.
- Add tutorial screens and game progression flow.

## Credits

- **Project creators:** Zildian Bendict Tablan and Vincent Aaron B. Vicete
- **Code:** Unity C# gameplay systems, UI management, damage handling, and level logic.
- **Design:** Class-based combat, skill menus, and boss mechanics.
- **Assets:** Some assets and sprites were sourced from Itch.io; credit and ownership remain with the original creators.
- **Tools:** Unity Editor, Universal Render Pipeline, Cinemachine, Input System.
