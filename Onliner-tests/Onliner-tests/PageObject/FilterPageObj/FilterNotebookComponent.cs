using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Onliner_tests.PageObject.FilterPageObj
{
    class FilterNotebookComponent : BasicFilterComponent
    {
        private WebDriver _driver;

        public FilterNotebookComponent(WebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public By ShowOtherCPU { get; set; } = By.XPath("//span[contains(text(),'Процессор')]//..//..//div[contains(@data-bind, 'facet.togglePopover')]");
        public By FilterPopoverVisible { get; set; } = By.XPath("//span[contains(text(),'Процессор')]/ancestor::*/following-sibling::div/child::div[contains(@class,'schema-filter-popover__wrapper')]/div");

        private string _cpuFormat = ".schema-filter-popover_visible input[value={0}]+span";

        public enum CpuType
        {
            IntelCoreI7,
            AMDa10,
            AMDfx,
            IntelAtom,
            Samsung
        }

        private By CreateOrderLocator(CpuType cpuType)
        {
            switch (cpuType)
            {
                case CpuType.IntelCoreI7:
                    return By.CssSelector(String.Format(_cpuFormat, "intelcorei7"));
                case CpuType.AMDa10:
                    return By.CssSelector(String.Format(_cpuFormat, "amda10"));
                case CpuType.AMDfx:
                    return By.CssSelector(String.Format(_cpuFormat, "amdfx"));
                case CpuType.IntelAtom:
                    return By.CssSelector(String.Format(_cpuFormat, "intelatom"));
                case CpuType.Samsung:
                    return By.CssSelector(String.Format(_cpuFormat, "samsung"));
                default:
                    throw new Exception($"Order type {cpuType.ToString()} not defined");
            }
        }

        public void SelectCPU(CpuType cpuType)
        {
            _driver.WaitForElementIsVisible(Filter);
            var proc = _driver.GetElement(ShowOtherCPU);
            _driver.Scroll((proc.Location.Y).ToString());
            if(!_driver.CheckClassForElement(FilterPopoverVisible, "schema-filter-popover_visible"))
            {
                _driver.Click(ShowOtherCPU);
            }
            _driver.WaitForElementIsVisible(FilterPopoverVisible);
            _driver.Click(CreateOrderLocator(cpuType));
            WaitComplitePrice();
        }

    }
}
