# Portal
An Open-Source Rappelz Private Server Launcher that brings worlds of new functionality to your Rappelz Private Server. Powered by
DataCore this launcher can make quick work of updating your client in more ways than one!

# Features (Currently Implemented)
- Powered by DataCore v3.0.0.5 (unreleased)
- New GUI [formerly Command Line] with Update, Connections, Logins [[For IMBC Login Mode]] statistics (server)
- Description settings Management (Server)
- Ability to disable updating (Server)
- Maintenance Mode (Server)
  - Stops user from Launching SFrame while Portal Maintenance Mode is toggled on
- Non-Traditional versioning system
  - Updates are compared on a case by case basis using SHA512
  - Update list is requested on demand and generated in real-time
- Self-Updating (Launcher updates it's own exe)
  - File is downloaded securely and verified by encrypted hash to prevent tampering
- Capability to mark updates for remote deletion (Incase of bad updates issued)
- Processes Data.000 / Resource-Folder updates seperately and/or side-by-side
- TCP/IP based Client <> Server model
  - All sensitive data encrypted (DES / RC4)
  - No sensitive information stored directly inside of Client (Decompiling will be useless)
  - Load remote IP/Port from config.opt
- Dual-Authentication mode
  - Launcher Login (imbc) and Client Login (normal)
- User authentication security
  - FingerPrint generated based on users computer hardware (used in banning)
- Dual Client Start Methods
  - Pre 8.1 (no bypass required)
  - 8.1+ (bypass required)
- BlankIndex (data.blk) tracking (used to recover space from orphaned files)
- Reimplemented rappelz_v1.opt editor (for editing client functions)
  - Reimplemented volume menu (includes expiremental toggle "lobby" theme and disable "music/bgm" repeat
- Unique ability to be run outside of the Rappelz Client folder
- Unique settings such as (always on-top, toggle fps (on sframe titlebar [windowed mode]) and close-on-start)

# Features (Future)

- (CLIENT) Multiple File Downloading Engines (via Google Drive, HTTP, FTP)
- (CLIENT) Defragment data.xxx files (essentially shrinking them by rebuilding them)
- (CLIENT) Ability to go into manual update mode (must install updates from .zip packages)
- (CLIENT) Ability to toggle on backups (All files will be compressed and moved to /backups/ and a restore point created in restore.opt
- (CLIENT) Ability to re-skin GUI
- (CLIENT) Shader menu (allows use of any epic shaders or custom shader file
