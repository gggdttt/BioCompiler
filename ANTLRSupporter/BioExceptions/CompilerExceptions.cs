
namespace ToolSupporter.BioExceptions
{
    enum CompilerExceptionMsgEnum : int
    {
        C_DROPLET_NOT_DECLARED = 1,
        C_DROPLET_DECLRATED_MORE_THAN_ONCE = 2,
        C_INCORRECT_SYNTAX = 3,
        C_VARIABLE_NOT_RELEASED = 4,
        C_VARIABLE_NOT_ASSIGNED_VALUE = 5
    }

    /// <summary>
    /// # d3 is not declared
    ///     droplet d1;
    ///     droplet d2; 
    ///     input(d3,10,10,3.2);
    /// </summary>
    public class DropletNotDeclaredException : BioException
    {
        public DropletNotDeclaredException(int line)
            : base(CompilerExceptionMsgEnum.C_DROPLET_NOT_DECLARED.ToString(), (int)CompilerExceptionMsgEnum.C_DROPLET_NOT_DECLARED, line)
        { }
    }

    /// <summary>
    /// droplet d1;
    /// droplet d1;
    /// </summary>
    public class DropletDeclaredMoreThanOnceException : BioException
    {
        public DropletDeclaredMoreThanOnceException(int line)
            : base(CompilerExceptionMsgEnum.C_DROPLET_DECLRATED_MORE_THAN_ONCE.ToString(), (int)CompilerExceptionMsgEnum.C_DROPLET_DECLRATED_MORE_THAN_ONCE, line)
        { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class IncorrectSyntaxException : BioException
    {
        public IncorrectSyntaxException(int line)
            : base(CompilerExceptionMsgEnum.C_INCORRECT_SYNTAX.ToString(), (int)CompilerExceptionMsgEnum.C_INCORRECT_SYNTAX, line)
        { }

        public IncorrectSyntaxException(string msg)
            : base((int)CompilerExceptionMsgEnum.C_INCORRECT_SYNTAX, msg)
        { }
    }

    public class VariableNotReleasedException : BioException
    {
        public VariableNotReleasedException(int line)
            : base(CompilerExceptionMsgEnum.C_VARIABLE_NOT_RELEASED.ToString(), (int)CompilerExceptionMsgEnum.C_VARIABLE_NOT_RELEASED, line)
        { }
    }

    public class VariableNotAssignedValueException : BioException
    {
        public VariableNotAssignedValueException(int line)
            : base(CompilerExceptionMsgEnum.C_VARIABLE_NOT_ASSIGNED_VALUE.ToString(), (int)CompilerExceptionMsgEnum.C_VARIABLE_NOT_ASSIGNED_VALUE, line)
        { }
    }
}