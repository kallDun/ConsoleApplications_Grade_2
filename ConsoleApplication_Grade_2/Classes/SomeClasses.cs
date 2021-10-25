using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication_Grade_2.Classes
{
    interface I
    {
        void IFoo();
    }

    abstract class A
    {
        public abstract void Bar();
    }

    abstract class B : A, I
    {
        public abstract void IFoo();

        public virtual void Method()
        {

        }
    }

    class C : B
    {
        public override void Bar()
        {

        }

        public override void IFoo()
        {

        }

        public override void Method()
        {

        }
    }

    class D : A, I
    {
        public override void Bar()
        {

        }

        public void IFoo()
        {

        }

        public void Foo()
        {

        }
    }

    class E : A
    {
        public override void Bar()
        {
            throw new NotImplementedException();
        }
    }
}
