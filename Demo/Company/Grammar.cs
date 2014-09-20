using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

namespace Demo.Company
{
    public static class Grammar
    {
        public static string Test()
        {
            ParserFn rule = Top();
            var state = new State("Microsoft { Ivan Ivanov, Peter Petrov, Sidor Sidorov }");
            StringBuilder result = new StringBuilder();
            
            ParseResult parseResult = state.Apply(rule);
            result.AppendLine(parseResult.Result.ToString());
            result.AppendLine("\r\n==============================================\r\n");
            result.AppendLine(state.debugInfo.ToString());

            return result.ToString();
        }

        public static ParserFn Top()
        {
            ParserFn companyName = Parser.RegEx(@"\w+", "company name");
            ParserFn employee = (
                Parser.RegEx(@"\w+", "first name") +
                Parser.String(" ") +
                Parser.RegEx(@"\w+", "last name"))
                .Select((List<object> values) => new Employee()
                {
                    Name = new PersonName()
                    {
                        FirstName = values[0].ToString(), 
                        LastName = values[2].ToString()
                    }
                });


            ParserFn employeeItem = (employee + Parser.Char(',').Optional() + Parser.RegEx(@"\s*"))
                .Select((List<object> values) => (Employee)values[0]);

            ParserFn company = (companyName + Parser.String(" { ") + employeeItem.AtLeastOnce() + Parser.Char('}'))
                .Select((List<object> values) => new Company() 
                { 
                    CompanyName = values[0].ToString(), 
                    Employees = ((List<object>)values[2]).Cast<Employee>()
                });

            return company;
        }
    }
}
