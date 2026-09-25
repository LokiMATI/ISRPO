import time
import pytest
from selenium import webdriver
from selenium.webdriver.common.by import By

@pytest.fixture(scope="function")
def browser():
    options = webdriver.ChromeOptions()
    options.set_capability('goog:loggingPrefs', {'browser': 'ALL'})
    
    driver = webdriver.Chrome(options=options)
    
    file_path = r"file://C:\temp\ispp31\ИСРПО\ЛР№11\LabWork10.html"
    driver.get(file_path)
    
    yield driver
    driver.quit()

def test_search_input(browser):
    assert browser.title != "", "Заголовок страницы пустой!"
    
    text_input = browser.find_element(By.NAME, "searchText")
    text_input.clear()
    
    search_text = "Тестовый ввод"
    text_input.send_keys(search_text)
    
    actual_text = text_input.get_attribute("value")
    assert actual_text == search_text, f"Ожидался ввод '{search_text}', но в поле находится '{actual_text}'"

def test_console_log_on_click(browser):
    button = browser.find_element(By.CLASS_NAME, "button-class")
    button.click()
    
    time.sleep(0.3)
    
    browser_logs = browser.get_log('browser')
    messages = [log['message'] for log in browser_logs]
    
    print("\nФактические логи из консоли Chrome:", messages)
    
    expected_message = '"Текущее значение count:" 1'

    assert any(expected_message in msg for msg in messages), \
        f"Сообщение '{expected_message}' не найдено в консоли браузера! Логи: {messages}"
