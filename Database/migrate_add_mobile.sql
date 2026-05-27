-- Run this if your apprentices table was created before mobile_number was added.

USE drdo_apprenticeship;

-- If you get "Duplicate column" error, mobile_number already exists — skip this script.
ALTER TABLE apprentices
  ADD COLUMN mobile_number VARCHAR(15) NOT NULL DEFAULT '+91 0000000000' AFTER department;
