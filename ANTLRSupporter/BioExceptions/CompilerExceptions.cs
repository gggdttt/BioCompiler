
namespace ToolSupporter.BioExceptions
{
    enum CompilerExceptionMsgEnum : int
    {
        C_DROPLET_NOT_DECLARED = 1,
        C_DROPLET_DECLRATED_MORE_THAN_ONCE = 2,
        C_INCORRECT_SYNTAX = 3,
        C_ILLEGAL_POSITION = 4,
        C_ILLEGAL_DROPLET_SIZE = 5
    }

    public class DropletNotDeclaredException : BioException
    {
        public DropletNotDeclaredException()
            : base(CompilerExceptionMsgEnum.C_DROPLET_NOT_DECLARED.ToString(), (int)CompilerExceptionMsgEnum.C_DROPLET_NOT_DECLARED)
        { }
    }

    public class DropletDeclaredMoreThanOnceException : BioException
    {
        public DropletDeclaredMoreThanOnceException()
            : base(CompilerExceptionMsgEnum.C_DROPLET_DECLRATED_MORE_THAN_ONCE.ToString(), (int)CompilerExceptionMsgEnum.C_DROPLET_DECLRATED_MORE_THAN_ONCE)
        { }
    }

    public class IncorrectSyntaxException : BioException
    {
        public IncorrectSyntaxException()
            : base(CompilerExceptionMsgEnum.C_INCORRECT_SYNTAX.ToString(), (int)CompilerExceptionMsgEnum.C_INCORRECT_SYNTAX)
        { }
    }

    public class IllegalPositionException : BioException
    {
        public IllegalPositionException()
            : base(CompilerExceptionMsgEnum.C_ILLEGAL_POSITION.ToString(), (int)CompilerExceptionMsgEnum.C_ILLEGAL_POSITION)
        { }
    }

    public class IllegalDropletSizeException : BioException
    {
        public IllegalDropletSizeException()
            : base(CompilerExceptionMsgEnum.C_ILLEGAL_DROPLET_SIZE.ToString(), (int)CompilerExceptionMsgEnum.C_ILLEGAL_DROPLET_SIZE)
        { }
    }
}
