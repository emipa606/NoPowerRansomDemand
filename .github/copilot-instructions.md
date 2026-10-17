# Copilot Instructions for No Power Ransom Demand (Continued) Mod

## Mod Overview and Purpose

**Mod Name**: No Power Ransom Demand (Continued)  
**Author**: newcolonist  
**PackageId**: Mlie.NoPowerRansomDemand  

The "No Power Ransom Demand (Continued)" mod is an enhancement of the original "No Power Ransom Demand" mod, introducing compatibility and dependency updates. This mod adjusts the vanilla 'ransom demand' incident in RimWorld to function without the need for powered comms consoles. It is specifically designed to integrate with mods like "No Power Comms Simplified," "Tribal Signal Fire (Continued)," or "Medieval Overhaul" to facilitate ransom demands without standard power requirements.

## Key Features and Systems

- **Harmony Patching**: The mod uses Harmony, a library that allows runtime modifications of the game's assembly code, to replace the logic of the vanilla ransom demand event.
- **Extended Compatibility**: Integrates with other mods that offer alternative communication methods that don't require power.
- **Seamless XML Integrations**: Includes adjustments via XML to extend game dynamics, adding new definitions necessary for non-standard ransom incidents.

## Coding Patterns and Conventions

- **File Structure**: Code is organized across four main C# files and one XML file. The C# files implement various patches and custom logic, while the XML file applies necessary game data alterations.
- **Naming Conventions**: Types and members are named consistently to improve readability and to signify their scope and purpose (e.g., `CommsConsoleUtility_PlayerHasPoweredCommsConsole`).
- **Patch Methodology**: The project applies Harmony patches using the postfix and prefix model, ensuring compatibility with other mods and vanilla game stability.

## XML Integration

- XML files are utilized to define new or patched game content. For this mod, the example file `SignalFire_TradeBeacon.xml` modifies definitions like `minifiedDef` and `ThingDef` to support the ransom mechanism through alternate communication methods.

## Harmony Patching

- **Dependencies**: The mod requires brrainz.harmony to create hooks into the game's incident logic.
- **Patch Descriptions**:
  * `CommsConsoleUtility_PlayerHasPoweredCommsConsole.cs`: Contains a postfix patch to recalibrate power checks for comms consoles.
  * `IncidentWorker_RansomDemand_CanFireNowSub.cs`: Features both prefix and postfix patches for the ransom demand incident worker.
  * `ReceiveLetter_Patch.cs`: Modifies the notification system for ransom demand events.
  * `TribalRansom.cs`: Centralizes utility methods related to the mod's primary functionality.

## Suggestions for Copilot

1. **Automated Harmony Patches**: Leverage Copilot to automate the generation of standard Harmony patches while adhering to consistent naming conventions.
2. **XML Definition Templates**: Use Copilot to streamline the creation of XML definition files, focusing on required elements such as `ThingDef`.
3. **Error Handling Stubs**: Implement try-catch blocks for possible null references or type mismatches when data is dynamically accessed.
4. **Separation of Concerns**: Engage Copilot in crafting functions that encapsulate single responsibilities to enhance code maintainability.
5. **Logging Functionality**: Enhance troubleshooting by inserting Copilot-suggested logging mechanisms, especially around significant incident trigger points.

By following these guidelines and leveraging GitHub Copilot, contributors can maintain the mod's robustness while efficiently adding new features or diagnosing issues.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

