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
- User authentication 
  - FingerPrint generated based on users computer hardware (used in banning)
- BlankIndex (data.blk) tracking (used to recover space from orphaned files)
