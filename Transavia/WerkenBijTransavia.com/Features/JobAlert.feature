Feature: JobAlert
    As a visitor of the site
    I want to see a button on every page
    So I can easily apply for a jobalert
    
Scenario Outline:  Jobalert banner visible on all pages
    Given I navigate to '<Menu Botton>, <Menu Item>'
    When I click Jobalert
    Then a new tab opens
    And I see the button 'Aanmelden Jobalert'
    
    Examples: 
    | Menu Botton    | Menu Item             |
    | Vakgebieden    | Cabine                |
    | Vakgebieden    | Technische dienst     |
    | Over Transavia | Transavia als bedrijf |
    | Vacatures      |                       |

Scenario: Jobalert can be configured
    Given I click Jobalert
    And I click the button 'Aanmelden Jobalert'
    When I fill out the form
      | Type      | Field                                                   | Value                 |
      | TextField | E-mailadres                                             | Test@TransaviaTest.nl |
      | Dropdown  | Voorkeursafdeling                                       | IT                    |
      | Radio     | Voorkeurstaal                                           | English               |
      | Radio     | Hoe vaak wil je vacaturemeldingen ontvangen?            | Zo spoedig mogelijk   |
      | Checkbox  | Ik ga ermee akkoord e-mails van Transavia te ontvangen. | true                  |
    Then I can submit the Jobalert