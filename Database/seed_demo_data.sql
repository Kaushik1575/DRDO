-- DRDO Apprenticeship - Demo data for Admin Panel
-- Run in MySQL Workbench AFTER init.sql
-- Demo login password for all apprentices below: Demo@123

USE drdo_apprenticeship;

-- Optional: clear existing apprentice demo rows (uncomment if re-running)
-- DELETE FROM apprentices WHERE apprentice_id LIKE 'APP-%';

-- Demo password hash for: Demo@123
SET @demo_hash = 'ziAJ+z4nj2LhsWjHHdNsmA==.I4gwrSiLfdcbDUJecAQOOjVnkcfParoRXkWA+vdfne0=';

INSERT IGNORE INTO apprentices (full_name, email, apprentice_id, department, mobile_number, apprenticeship_password_hash, is_active) VALUES
('Linda Smith', 'linda.smith1@example-apprentice.com', 'APP-1001', 'Data Science', '+91 9876501001', @demo_hash, 0),
('Joshua Garcia', 'joshua.garcia10@example-apprentice.com', 'APP-1010', 'Data Science', '+91 9876501010', @demo_hash, 1),
('Paul Anderson', 'paul.anderson29@example-apprentice.com', 'APP-1029', 'Data Science', '+91 9876501029', @demo_hash, 1),
('William Wilson', 'william.wilson31@example-apprentice.com', 'APP-1031', 'Data Science', '+91 9876501031', @demo_hash, 1),
('David Lopez', 'david.lopez39@example-apprentice.com', 'APP-1039', 'Data Science', '+91 9876501039', @demo_hash, 0),
('Richard Anderson', 'richard.anderson50@example-apprentice.com', 'APP-1050', 'Data Science', '+91 9876501050', @demo_hash, 1),
('Priya Sharma', 'priya.sharma@drdo-apprentice.com', 'APP-1051', 'Electronics', '+91 9876501051', @demo_hash, 1),
('Arjun Mehta', 'arjun.mehta@drdo-apprentice.com', 'APP-1052', 'Mechanical', '+91 9876501052', @demo_hash, 1),
('Sneha Reddy', 'sneha.reddy@drdo-apprentice.com', 'APP-1053', 'Computer Science', '+91 9876501053', @demo_hash, 0),
('Rahul Kumar', 'rahul.kumar@drdo-apprentice.com', 'APP-1054', 'Aerospace', '+91 9876501054', @demo_hash, 1),
('Anita Desai', 'anita.desai@drdo-apprentice.com', 'APP-1055', 'Electronics', '+91 9876501055', @demo_hash, 1),
('Vikram Singh', 'vikram.singh@drdo-apprentice.com', 'APP-1056', 'Data Science', '+91 9876501056', @demo_hash, 0),
('Kavya Nair', 'kavya.nair@drdo-apprentice.com', 'APP-1057', 'Chemical', '+91 9876501057', @demo_hash, 1),
('Mohit Patel', 'mohit.patel@drdo-apprentice.com', 'APP-1058', 'Mechanical', '+91 9876501058', @demo_hash, 1),
('Divya Iyer', 'divya.iyer@drdo-apprentice.com', 'APP-1059', 'Computer Science', '+91 9876501059', @demo_hash, 0);

-- Verify counts (should show Total=15, Active=10, Inactive=5)
SELECT
  COUNT(*) AS total_apprentices,
  SUM(is_active) AS active_apprentices,
  SUM(CASE WHEN is_active = 0 THEN 1 ELSE 0 END) AS inactive_apprentices
FROM apprentices;

SELECT apprentice_id, full_name, department, email, mobile_number,
       CASE WHEN is_active = 1 THEN 'Active' ELSE 'Inactive' END AS status
FROM apprentices
ORDER BY apprentice_id;
