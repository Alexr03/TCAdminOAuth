DELETE FROM tc_modules WHERE module_id LIKE '57d6df06-41b6-4b76-800c-e446eed1bbad';
DELETE FROM tc_page_icons WHERE module_id LIKE '57d6df06-41b6-4b76-800c-e446eed1bbad';
DELETE FROM tc_site_map WHERE module_id LIKE '57d6df06-41b6-4b76-800c-e446eed1bbad';
DELETE FROM tc_panelbar_categories WHERE module_id LIKE '57d6df06-41b6-4b76-800c-e446eed1bbad';

DELETE FROM tc_info WHERE name LIKE '%.Creds';
DELETE FROM tc_info WHERE name LIKE 'OAuth.Global.Settings';