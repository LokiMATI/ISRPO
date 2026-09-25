import os
import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By

@pytest.fixture(scope="function")
def browser():
    driver = webdriver.Chrome()
    file_path = r"file://C:\temp\ispp31\ИСРПО\ЛР№11\LabWork10.html"
    driver.get(file_path)
    yield driver
    driver.quit()

def test_page_header(browser):
    header_element = browser.find_element(By.ID, "header")
    
    expected_text = "Тестирование с DevTools" 
    
    assert header_element.text == expected_text, f"Ожидался текст '{expected_text}', но получен '{header_element.text}'"

def test_page_error_message(browser):
    error_message_element = browser.find_element(By.ID, "error-message")
    
    expected_text = "Ошибка не произошла" 
    
    assert error_message_element.text == expected_text, f"Ожидался текст '{expected_text}', но получен '{header_element.text}'"
