# Portal
An Open-Source Rappelz Private Server Launcher that brings worlds of new functionality to your Rappelz Private Server. Powered by
DataCore this launcher can make quick work of updating your client in more ways than one!

# Features (Currently Implemented)
- Powered by DataCore v3.0.0.5 (unreleased)
- Non-Traditional versioning system
  - File list kept @ server in updates.opt (format: hashed-name|file-sha512|isLegacy)
- Non-Tradition storage system
  - All updates are kept unhashed in a Google Drive accessed via Google Service Account (see google apis)
- Capability to mark updates for remote deletion (Incase of bad updates issued)
- TCP/IP based Client <> Server model
  - All sensitive data encrypted (DES / RC4)
  - No sensitive information stored directly inside of Client (Decompiling will be useless)
  - Load remote IP/Port from config.opt
- User authentication 
  - FingerPrint generated based on users computer hardware (used in banning)
- Client auth-login (using OTP)
  - Tradition login also available
- BlankIndex (data.blk) tracking (used to recover space from orphaned files)
- Reimplemented rappelz_v1.opt editor (for editing client functions)
  - Reimplemented volume menu (includes expiremental toggle "lobby" theme and disable "music/bgm" repeat
- Unique ability to be run outside of the Rappelz Client folder

# Features (Future)
- (CLIENT) Self-Updating (being reimplemented)
  - Self-Updater is also self-updating
- (CLIENT) Multiple File Downloading Engines (via Google Drive, HTTP, FTP and eventually TCP)
- (CLIENT) Defragment data.xxx files (essentially shrinking them by rebuilding them)
- (CLIENT) Ability to go into manual update mode (must install updates from .zip packages)
- (CLIENT) Ability to toggle on backups (All files will be compressed and moved to /backups/ and a restore point created in restore.opt
- (CLIENT) Ability to re-skin GUI
- (CLIENT/SERVER) Ability to toggle off user authentication
- (CLIENT) Shader menu (allows use of any epic shaders or custom shader files
- (CLIENT) Unique settings such as (always on-top, toggle fps (on sframe titlebar [windowed mode]) and close-on-start)
