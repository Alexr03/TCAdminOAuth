INSERT INTO tc_modules (module_id, display_name, version, enabled, config_page, component_directory,
                        security_class)
VALUES ('57d6df06-41b6-4b76-800c-e446eed1bbad', 'OAuth Authentication', '2.0', 1, null, null, null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (1, '57d6df06-41b6-4b76-800c-e446eed1bbad', null, null, null, '', '', 'OAuth', 'Login', 'Login',
        'MenuIcons/Base/Info24x24.png', null, 0, null, null, null, null, null, '');
INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (2, '57d6df06-41b6-4b76-800c-e446eed1bbad', null, null, null, '', '', 'OAuth', 'Callback', 'Login Callback',
        'MenuIcons/Base/Info24x24.png', null, 0, null, null, null, null, null, '');
INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url,
                         controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar,
                         view_order, required_permissions, menu_required_permissions, page_manager,
                         page_search_provider, cache_name)
VALUES (3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 40, '07405876-e8c2-4b24-a774-4ef57f596384', 1, '/OAuth/Edit',
        '/OAuth/Edit', 'OAuth', 'Edit', 'OAuth Settings', 'MenuIcons/Base/ServerComponents24x24.png',
        'MenuIcons/Base/ServerComponents16x16.png', 1, 1000, '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})
({57d6df06-41b6-4b76-800c-e446eed1bbad,1,0})
', '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})
({57d6df06-41b6-4b76-800c-e446eed1bbad,1,0})
', null, null, '');

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_page_icons (icon_id, module_id, page_id, linked_page_id, linked_page_module_id, display_name,
                           description, icon, url, display_sql, user_type, selected_user_type, view_order,
                           enabled, icon_manager, is_postback, postback_class, new_page)
VALUES (4, '57d6df06-41b6-4b76-800c-e446eed1bbad', 3, 3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'Discord OAuth',
        'Modify discord OAuth Settings', 'MenuIcons/Base/ServerNetwork.png', '/OAuth/Edit?provider=Discord', null, 0,
        null, 100, 1, null, null, null, null);
INSERT INTO tc_page_icons (icon_id, module_id, page_id, linked_page_id, linked_page_module_id, display_name,
                           description, icon, url, display_sql, user_type, selected_user_type, view_order,
                           enabled, icon_manager, is_postback, postback_class, new_page)
VALUES (3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 3, 3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'WHMCS OAuth',
        'Modify whmcs OAuth Settings', 'MenuIcons/Base/ServerNetwork.png', '/OAuth/Edit?provider=WHMCS', null, 0, null,
        100, 1, null, null, null, null);
INSERT INTO tc_page_icons (icon_id, module_id, page_id, linked_page_id, linked_page_module_id, display_name,
                           description, icon, url, display_sql, user_type, selected_user_type, view_order,
                           enabled, icon_manager, is_postback, postback_class, new_page)
VALUES (2, '57d6df06-41b6-4b76-800c-e446eed1bbad', 3, 3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'Google OAuth',
        'Modify google OAuth Settings', 'MenuIcons/Base/ServerNetwork.png', '/OAuth/Edit?provider=Google', null, 0,
        null, 100, 1, null, null, null, null);
INSERT INTO tc_page_icons (icon_id, module_id, page_id, linked_page_id, linked_page_module_id, display_name,
                           description, icon, url, display_sql, user_type, selected_user_type, view_order,
                           enabled, icon_manager, is_postback, postback_class, new_page)
VALUES (1, '57d6df06-41b6-4b76-800c-e446eed1bbad', 3, 3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'OAuth Information',
        'Information about OAuth', 'MenuIcons/Base/ServerNetwork.png', '/OAuth/Edit', null, 0, null, 1, 1, null, null,
        null, null);
INSERT INTO tc_page_icons (icon_id, module_id, page_id, linked_page_id, linked_page_module_id, display_name,
                           description, icon, url, display_sql, user_type, selected_user_type, view_order,
                           enabled, icon_manager, is_postback, postback_class, new_page)
VALUES (5, '57d6df06-41b6-4b76-800c-e446eed1bbad', 3, 3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'Github OAuth',
        'Modify github OAuth Settings', 'MenuIcons/Base/ServerNetwork.png', '/OAuth/Edit?provider=Github', null, 0,
        null, 100, 1, null, null, null, null);
INSERT INTO tc_page_icons (icon_id, module_id, page_id, linked_page_id, linked_page_module_id, display_name,
                           description, icon, url, display_sql, user_type, selected_user_type, view_order,
                           enabled, icon_manager, is_postback, postback_class, new_page)
VALUES (6, '57d6df06-41b6-4b76-800c-e446eed1bbad', 3, 3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'Facebook OAuth',
        'Modify facebook OAuth Settings', 'MenuIcons/Base/ServerNetwork.png', '/OAuth/Edit?provider=Facebook', null, 0,
        null, 100, 1, null, null, null, null);
INSERT INTO tc_page_icons (icon_id, module_id, page_id, linked_page_id, linked_page_module_id, display_name,
                           description, icon, url, display_sql, user_type, selected_user_type, view_order,
                           enabled, icon_manager, is_postback, postback_class, new_page)
VALUES (7, '57d6df06-41b6-4b76-800c-e446eed1bbad', 3, 3, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'OAuth Settings',
        'Modify OAuth Settings', 'MenuIcons/Base/ServerNetwork.png', '/OAuth/OAuthSettings', null, 0, null, 2, 1, null,
        null, null, null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_panelbar_categories (category_id, module_id, display_name, view_order, parent_category_id,
                                    parent_module_id, page_id, panelbar_icon)
VALUES (1, '57d6df06-41b6-4b76-800c-e446eed1bbad', 'OAuth', 1002, 6, '07405876-e8c2-4b24-a774-4ef57f596384', null,
        null);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_permission_categories (category_id, module_id, parent_category_id, parent_module_id, display_name,
                                      view_order)
VALUES (1, '57d6df06-41b6-4b76-800c-e446eed1bbad', null, null, 'OAuth Credentials', 1060);

# ----------------------------------------------------------------------------------------------------------------------

INSERT INTO tc_permissions (permission_id, module_id, category_id, display_name, permission_type, view_order,
                                    role_owner_required_permissions, same_role_required_permissions, top_level_only)
VALUES (1, '57d6df06-41b6-4b76-800c-e446eed1bbad', 1, 'Modify OAuth Credentials', 1, 1000, '', null, 1);