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
            ParserFn<Company> rule = Top();
            var state = new State("Microsoft { Ivan Ivanov, Peter Petrov, Sidor Sidorov }");
            IParseResult<Company> parseResult = state.Apply(rule);

            StringBuilder result = new StringBuilder();
            result.AppendLine(parseResult.Result.ToString());
            result.AppendLine("\r\n=============================\r\n");
            result.AppendLine(state.debugInfo.ToString());

            return result.ToString();
        }
        
        public static ParserFn<Company> Top()
        {
            ParserFn<string> companyName = Parser.RegEx(@"\w+", "company name");

            ParserFn<Employee> employee = (Parser.RegEx(@"\w+", "first name").ToObj() + Parser.String(" ").ToObj() + Parser.RegEx(@"\w+", "last name").ToObj())
                .Select((List<object> values) => new Employee() {Name = new PersonName() {FirstName = values[0].ToString(), LastName = values[2].ToString()}});

            ParserFn<Employee> employeeItem = (employee.ToObj() + Parser.Char(',').Optional().ToObj() + Parser.RegEx(@"\s*").ToObj())
                .Select((List<object> values) => (Employee)values[0]);

            ParserFn<Company> company = (companyName.ToObj() + Parser.String(" { ").ToObj() + employeeItem.AtLeastOnce().ToObj() + Parser.Char('}').ToObj())
                .Select((List<object> values) => new Company
                {
                    CompanyName = values[0].ToString(), 
                    Employees = (List<Employee>)values[2]
                });

            return company;
        }


    }
}
