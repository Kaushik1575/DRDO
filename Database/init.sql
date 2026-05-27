-- DRDO Apprenticeship Management System - MySQL setup
-- Run in MySQL Workbench (Local instance MySQL80) after connecting as root.

CREATE DATABASE IF NOT EXISTS drdo_apprenticeship
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE drdo_apprenticeship;

-- Admin table (Admin Register / Admin Login)
CREATE TABLE IF NOT EXISTS admins (
    id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    department VARCHAR(100) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_active TINYINT(1) NOT NULL DEFAULT 1
);

-- Apprentice table (Apprenticeship Register / Login + Admin Panel)
CREATE TABLE IF NOT EXISTS apprentices (
    id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    apprentice_id VARCHAR(20) NOT NULL UNIQUE,
    department VARCHAR(100) NOT NULL COMMENT 'Trade/Field shown in admin panel',
    mobile_number VARCHAR(15) NOT NULL,
    apprenticeship_password_hash VARCHAR(255) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_active TINYINT(1) NOT NULL DEFAULT 1
);
