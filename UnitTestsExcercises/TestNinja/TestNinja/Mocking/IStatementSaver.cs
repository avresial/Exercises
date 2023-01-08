using System;

namespace TestNinja.Mocking
{
    public interface IStatementSaver
    {
        string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }
}