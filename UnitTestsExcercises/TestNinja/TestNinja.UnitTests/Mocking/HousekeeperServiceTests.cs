using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class HousekeeperServiceTests
    {
        private string statementFilename;
        private Mock<IStatementSaver> statementSaver = new Mock<IStatementSaver>();
        private Mock<IEmailSender> emailService = new Mock<IEmailSender>();
        private Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        private Mock<IXtraMessageBox> xtraMessageBox = new Mock<IXtraMessageBox>();
        private HousekeeperService housekeeperService;
        private DateTime statementDate = new DateTime(2022, 1, 1);
        private Housekeeper housekeeper;

        public HousekeeperServiceTests()
        {
            housekeeper = new Housekeeper() { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            statementFilename = "filename";
            statementSaver.Setup(g => g.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate))
                          .Returns(() => statementFilename);

            unitOfWork.Setup(x => x.Query<Housekeeper>())
                        .Returns(new List<Housekeeper> { housekeeper }
                        .AsQueryable);
            
            housekeeperService = new HousekeeperService(statementSaver.Object, emailService.Object, unitOfWork.Object, xtraMessageBox.Object);
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_ShouldGeneratesStatements()
        {
            housekeeperService.SendStatementEmails(statementDate);

            statementSaver.Verify(g => g.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SendStatementEmails_WhenParameterIsInvalid_ShouldNotGenerateStatement(string email)
        {
            housekeeper.Email = email;

            housekeeperService.SendStatementEmails(statementDate);

            statementSaver.Verify(g => g.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate), Times.Never);
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_EmailtheStatement()
        {
            housekeeperService.SendStatementEmails(statementDate);

            VerifyEmailWasSent();
        }



        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SendStatementEmails_WhensatementFileIsInvalid_ShouldNotEmailTheStatement(string statement)
        {
            statementFilename = statement;

            housekeeperService.SendStatementEmails(statementDate);

            VerifyEmailWasNotSend();
        }

        [Fact]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            emailService.Setup(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            housekeeperService.SendStatementEmails(statementDate);

            xtraMessageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailWasSent()
        {
            emailService.Verify(es => es.EmailFile(housekeeper.Email, housekeeper.StatementEmailBody, statementFilename, It.IsAny<string>()), Times.Once);
        }

        private void VerifyEmailWasNotSend()
        {
            emailService.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
