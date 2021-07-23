Feature: Todos can have Notifications
    As a User,
    I want to have notifications for my todos

Scenario: Notifications should appear for Todos that have reached their target dates
    Given a new user is added to the database with the username aastha and the email aastha@bmw.de
    When a todo is added to the user with the target date of 01.01.2021
    Then if the user logs in he will have 1 notifications

Scenario: Notifications should not appear for Todos that have reached their target dates and are complete
    Given a new user is added to the database with the username jiawei and the email jiawei@bmw.de
    When a todo is added to the user with the target date of 01.01.2021
    And the todo is set to complete
    Then if the user logs in he will have 0 notifications

Scenario: Notifications should not appear for Todos that have not reached their target date
    Given a new user is added to the database with the username jiawei and the email jiawei@bmw.de
    When a todo is added to the user with the target date of 01.01.2022
    Then if the user logs in he will have 0 notifications
    
Scenario: Notifications should work with x notifications
    Given a new user is added to the database with the username jiawei and the email jiawei@bmw.de
    When I have 2 new notifications
    Then I expect to be able to see 2 notifications