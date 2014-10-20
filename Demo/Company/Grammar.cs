using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Atomics;
using Combinator.Common;

namespace Demo.Company
{
    public static class Grammar
    {
        public static string Test()
        {
            Parser rule = Top();
            var state = new State("Microsoft { Ivan Ivanov, Peter Petrov, Sidor Sidorov }");
            StringBuilder result = new StringBuilder();
            
            ParseResult parseResult = state.Apply(rule);
            result.AppendLine(parseResult.Result.ToString());
            result.AppendLine("\r\n==============================================\r\n");
            result.AppendLine(state.debugInfo.ToString());

            return result.ToString();
        }

        public static Parser Top()
        {
            var companyName = new Rule("companyName");
            var employee = new Rule("employee");
            var employeeItem = new Rule("employeeItem");
            var company = new Rule("company");
            
            
            companyName.Expr = Parsers.RegEx(@"\w+");
            employee.Expr = (Parsers.RegEx(@"\w+") + Parsers.String(" ") + Parsers.RegEx(@"\w+"))
                .Select((List<object> values) => new Employee()
                {
                    Name = new PersonName()
                    {
                        FirstName = values[0].ToString(), 
                        LastName = values[2].ToString()
                    }
                });


            employeeItem.Expr = (employee + Parsers.Char(',').Optional() + Parsers.RegEx(@"\s*"))
                .Select((List<object> values) => (Employee)values[0]);

            company.Expr = (companyName + Parsers.String(" { ") + employeeItem.AtLeastOnce() + Parsers.Char('}'))
                .Select((List<object> values) => new Company() 
                { 
                    CompanyName = values[0].ToString(), 
                    Employees = ((List<object>)values[2]).Cast<Employee>()
                });

            return company;
        }
    }
}
