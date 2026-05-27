-- Migration: Add mobile_number column to apprentices table
-- Date: 2026-05-26

ALTER TABLE `apprentices`
ADD COLUMN `mobile_number` VARCHAR(15) NOT NULL DEFAULT '' AFTER `department`;

-- Create an index for mobile_number if needed
-- CREATE INDEX idx_mobile_number ON `apprentices`(`mobile_number`);
