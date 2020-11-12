using System;
using System.Collections.Generic;

namespace MsLab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================TASK1==================================");
            Task1();
            Console.WriteLine("==================================TASK2==================================");
            Task2();
            Console.WriteLine("==================================TASK3==================================");
            Task3();
            Console.ReadLine();
        }

        public static void Task1()
        {
            Transition genA = new Transition("Згенерировать A");
            Transition queryAB = new Transition("Створити запит A-B");
            Transition replyBA = new Transition("Отримати відповідь B-A");
            Transition sendAB = new Transition("Надіслати повідомлення A-B");
            Transition getInB = new Transition("Отримати в В");
            Transition informGetInB = new Transition("Успішний прийом в B");
            //
            Transition genB = new Transition("Згенерировать B");
            Transition queryBA = new Transition("Створити запит B-A");
            Transition replyAB = new Transition("Отримати відповідь A-B");
            Transition sendBA = new Transition("Надіслати повідомлення B-A");
            Transition getInA = new Transition("Отримати в A");
            Transition informGetInA = new Transition("Успішний прийом в A");

            Position indicator = new Position("Дозвіл на відправку", 1);
            Position incomingA = new Position("прийшло в A", 1);
            Position generatedA = new Position("згенероване A", 0);
            Position requestedA = new Position("запит A", 0);
            Position admitedA = new Position("дозволено A", 0);
            Position sentA = new Position("надіслано A", 0);
            Position gotB = new Position("прийшло B", 0);
            Position allGotB = new Position("усі оброблені  B", 0);
            //
            Position incomingB = new Position("прийшло в B", 1);
            Position generatedB = new Position("згенероване B", 0);
            Position requestedB = new Position("запит B", 0);
            Position admitedB = new Position("дозволено  B", 0);
            Position sentB = new Position("надіслано B", 0);
            Position gotA = new Position("прийшло A", 0);
            Position allGotA = new Position("усі оброблені  A", 0);

            Arc indicator_replyBA = new Arc("ind-rba", indicator, replyBA, 1);
            Arc incomingA_genA = new Arc("inA-genA", incomingA, genA, 1);
            Arc genA_incomingA = new Arc("genA-inA", incomingA, 1);
            Arc genA_generatedA = new Arc("genA-gendA", generatedA, 1);
            Arc generatedA_queryAB = new Arc("gendA-qab", generatedA, queryAB, 1);
            Arc queryAB_requestedA = new Arc("qab-reqA", requestedA, 1);
            Arc requestedA_replyBA = new Arc("reqA-rba", requestedA, replyBA, 1);
            Arc replyBA_admitedA = new Arc("rba-admA", admitedA, 1);
            Arc admitedA_sendAB = new Arc("admA-sendAB", admitedA, sendAB, 1);
            Arc sendAB_sentA = new Arc("sendAB-sentA", sentA, 1);
            Arc sentA_getInB = new Arc("sentA-getinB", sentA, getInB, 1);
            Arc getInB_gotB = new Arc("getinB_gotB", gotB, 1);
            Arc gotB_infGotInB = new Arc("gotB_infgB", gotB, informGetInB, 1);
            Arc infGotInB_indicator = new Arc("infgB_ind", indicator, 1);
            Arc infGotInB_allGotB = new Arc("infgB_allGotB", allGotB, 1);
            //
            Arc indicator_replyAB = new Arc("ind-rab", indicator, replyAB, 1);
            Arc incomingB_genB = new Arc("inB-genB", incomingB, genB, 1);
            Arc genB_incomingB = new Arc("genB-inB", incomingB, 1);
            Arc genB_generatedB = new Arc("genB-gendB", generatedB, 1);
            Arc generatedB_queryBA = new Arc("gendB-qba", generatedB, queryBA, 1);
            Arc queryBA_requestedB = new Arc("qba-reqB", requestedB, 1);
            Arc requestedB_replyAB = new Arc("reqB-rab", requestedB, replyAB, 1);
            Arc replyAB_admitedB = new Arc("rab-admB", admitedB, 1);
            Arc admitedB_sendBA = new Arc("admB-sendBA", admitedB, sendBA, 1);
            Arc sendBA_sentB = new Arc("sendBA-sentB", sentB, 1);
            Arc sentB_getInA = new Arc("sentB-getinA", sentB, getInA, 1);
            Arc getInA_gotA = new Arc("getinA_gotA", gotA, 1);
            Arc gotA_infGotInA = new Arc("gotA_infgA", gotA, informGetInA, 1);
            Arc infGotInA_indicator = new Arc("infgA_ind", indicator, 1);
            Arc infGotInA_allGotA = new Arc("infgA_allGotA", allGotA, 1);

            genA.InCommingArcs.Add(incomingA_genA);
            genA.OutCommingArcs.Add(genA_generatedA);
            genA.OutCommingArcs.Add(genA_incomingA);
            queryAB.InCommingArcs.Add(generatedA_queryAB);
            queryAB.OutCommingArcs.Add(queryAB_requestedA);
            replyBA.InCommingArcs.Add(requestedA_replyBA);
            replyBA.InCommingArcs.Add(indicator_replyBA);
            replyBA.OutCommingArcs.Add(replyBA_admitedA);
            sendAB.InCommingArcs.Add(admitedA_sendAB);
            sendAB.OutCommingArcs.Add(sendAB_sentA);
            getInB.InCommingArcs.Add(sentA_getInB);
            getInB.OutCommingArcs.Add(getInB_gotB);
            informGetInB.InCommingArcs.Add(gotB_infGotInB);
            informGetInB.OutCommingArcs.Add(infGotInB_allGotB);
            informGetInB.OutCommingArcs.Add(infGotInB_indicator);
            
            genB.InCommingArcs.Add(incomingB_genB);
            genB.OutCommingArcs.Add(genB_generatedB);
            genB.OutCommingArcs.Add(genB_incomingB);
            queryBA.InCommingArcs.Add(generatedB_queryBA);
            queryBA.OutCommingArcs.Add(queryBA_requestedB);
            replyAB.InCommingArcs.Add(requestedB_replyAB);
            replyAB.InCommingArcs.Add(indicator_replyAB);
            replyAB.OutCommingArcs.Add(replyAB_admitedB);
            sendBA.InCommingArcs.Add(admitedB_sendBA);
            sendBA.OutCommingArcs.Add(sendBA_sentB);
            getInA.InCommingArcs.Add(sentB_getInA);
            getInA.OutCommingArcs.Add(getInA_gotA);
            informGetInA.InCommingArcs.Add(gotA_infGotInA);
            informGetInA.OutCommingArcs.Add(infGotInA_allGotA);
            informGetInA.OutCommingArcs.Add(infGotInA_indicator);

            List<Position> places = new List<Position>() { incomingA, generatedA, requestedA, admitedA, sentA, gotB, allGotB, indicator,
                                             incomingB, generatedB, requestedB, admitedB, sentB, gotA, allGotA};
            List<Transition> transitions = new List<Transition>() { genA, queryAB, replyBA, sendAB, getInB, informGetInB,
                                                  genB, queryBA, replyAB, sendBA, getInA, informGetInA};
            Model task1 = new Model(places, transitions);
            task1.Simulate(100,true);
            Console.WriteLine();
            Console.WriteLine("Got in B amount: {0}", allGotB.CurrentNumberOfMarkers);
            Console.WriteLine("Got in A amount: {0}", allGotA.CurrentNumberOfMarkers);
        }

        public static void Task2()
        {
            int n;
            Console.WriteLine("Enter buffer max:");
            n = Convert.ToInt32(Console.ReadLine());

            Transition processor = new Transition("Processor");
            Transition consumer = new Transition("Consumer");

            Position incoming = new Position("Incoming", 1);
            Position buffer = new Position("Buffer", 0);
            Position stopProcessorRule = new Position("Free in buffer", n);
            Position consumedNumber = new Position("Consumed", 0);

            Arc incoming_processor = new Arc("inc-proc", incoming, processor, 1);
            Arc processor_buffer = new Arc("put", buffer, 1);
            Arc processor_incoming = new Arc("proc-inc", incoming, 1);
            Arc buffer_consumer = new Arc("take", buffer, consumer, 1);
            Arc consumer_stoprule = new Arc("cons-spr", stopProcessorRule, 1);
            Arc stoprule_processor = new Arc("spr-proc", stopProcessorRule, processor, 1);
            Arc consumer_consumed = new Arc("cons-count", consumedNumber, 1);

            processor.InCommingArcs.Add(incoming_processor);
            processor.InCommingArcs.Add(stoprule_processor);
            processor.OutCommingArcs.Add(processor_buffer);
            processor.OutCommingArcs.Add(processor_incoming);
            consumer.InCommingArcs.Add(buffer_consumer);
            consumer.OutCommingArcs.Add(consumer_stoprule);
            consumer.OutCommingArcs.Add(consumer_consumed);

            List<Position> places = new List<Position>() { incoming, buffer, stopProcessorRule, consumedNumber };
            List<Transition> transitions = new List<Transition>() { processor, consumer };

            Model task2 = new Model(places, transitions);
            task2.Simulate(100,true);
            Console.WriteLine();
            Console.WriteLine("Average markers in buffer: {0}", buffer.AvarageCountOfMarkers    .ToString());
        }

        public static void Task3()
        {
            int n;
            Console.WriteLine("Enter resources number (>3):");
            n = Convert.ToInt32(Console.ReadLine());

            Transition type_1_create = new Transition("Створити 1 типу");
            Transition type_1_process = new Transition("Виконати 1 типу");
            Transition type_2_create = new Transition("Створити 2 типу");
            Transition type_2_process = new Transition("Виконати 2 типу");
            Transition type_3_create = new Transition("Створити 3 типу");
            Transition type_3_process = new Transition("Виконати 3 типу");

            Position resources = new Position("Ресурс", n);
            Position incoming1 = new Position("Incoming t1", 1);
            Position incoming2 = new Position("Incoming t2", 1);
            Position incoming3 = new Position("Incoming t3", 1);
            Position created1 = new Position("Створено t1", 0);
            Position created2 = new Position("Створено t2", 0);
            Position created3 = new Position("Створено t3", 0);
            Position processed1 = new Position("Виконано t1", 0);
            Position processed2 = new Position("Виконано t2", 0);
            Position processed3 = new Position("Винонано t3", 0);

            Arc incoming1_create1 = new Arc("inc1-cr1", incoming1, type_1_create, 1);
            Arc create1_incoming1 = new Arc("cr1-inc1", incoming1, 1);
            Arc create1_created1 = new Arc("cr1-crd1", created1, 1);
            Arc created1_process1 = new Arc("crd1-pr1", created1, type_1_process, 1);
            Arc resources_process1 = new Arc("r-pr1", resources, type_1_process, n);
            Arc process1_resources = new Arc("pr1-r", resources, n);
            Arc process1_processed1 = new Arc("pr1-prd1", processed1, 1);
            //
            Arc incoming2_create2 = new Arc("inc2-cr2", incoming2, type_2_create, 1);
            Arc create2_incoming2 = new Arc("cr2-inc2", incoming2, 1);
            Arc create2_created2 = new Arc("cr2-crd2", created2, 1);
            Arc created2_process2 = new Arc("crd2-pr2", created2, type_2_process, 1);
            Arc resources_process2 = new Arc("r-pr2", resources, type_2_process, n / 3);
            Arc process2_resources = new Arc("pr2-r", resources, n / 3);
            Arc process2_processed2 = new Arc("pr2-prd2", processed2, 1);
            //
            Arc incoming3_create3 = new Arc("inc3-cr3", incoming3, type_3_create, 1);
            Arc create3_incoming3 = new Arc("cr3-inc3", incoming3, 1);
            Arc create3_created3 = new Arc("cr3-crd3", created3, 1);
            Arc created3_process3 = new Arc("crd3-pr3", created3, type_3_process, 1);
            Arc resources_process3 = new Arc("r-pr3", resources, type_3_process, n / 2);
            Arc process3_resources = new Arc("pr3-r", resources, n / 2);
            Arc process3_processed3 = new Arc("pr3-prd3", processed3, 1);

            type_1_create.InCommingArcs.Add(incoming1_create1);
            type_1_create.OutCommingArcs.Add(create1_incoming1);
            type_1_create.OutCommingArcs.Add(create1_created1);
            type_1_process.InCommingArcs.Add(created1_process1);
            type_1_process.InCommingArcs.Add(resources_process1);
            type_1_process.OutCommingArcs.Add(process1_processed1);
            type_1_process.OutCommingArcs.Add(process1_resources);

            type_2_create.InCommingArcs.Add(incoming2_create2);
            type_2_create.OutCommingArcs.Add(create2_incoming2);
            type_2_create.OutCommingArcs.Add(create2_created2);
            type_2_process.InCommingArcs.Add(created2_process2);
            type_2_process.InCommingArcs.Add(resources_process2);
            type_2_process.OutCommingArcs.Add(process2_processed2);
            type_2_process.OutCommingArcs.Add(process2_resources);

            type_3_create.InCommingArcs.Add(incoming3_create3);
            type_3_create.OutCommingArcs.Add(create3_incoming3);
            type_3_create.OutCommingArcs.Add(create3_created3);
            type_3_process.InCommingArcs.Add(created3_process3);
            type_3_process.InCommingArcs.Add(resources_process3);
            type_3_process.OutCommingArcs.Add(process3_processed3);
            type_3_process.OutCommingArcs.Add(process3_resources);

            List<Position> places = new List<Position>() { resources, incoming1, incoming2, incoming3,
                                                        created1, created2, created3,
                                                        processed1, processed2, processed3};
            List<Transition> transitions = new List<Transition>() { type_1_create, type_1_process, type_2_create, type_2_process, type_3_create, type_3_process };
            Model task3 = new Model(places, transitions);
            task3.Simulate(100,true);
            Console.WriteLine();
            int allProcessed = processed1.CurrentNumberOfMarkers + processed2.CurrentNumberOfMarkers + processed3.CurrentNumberOfMarkers;
            Console.WriteLine("Processed amount: {0}", allProcessed);
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "Type", "Processed", "% of all");
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "1", processed1.CurrentNumberOfMarkers, (((Double)processed1.CurrentNumberOfMarkers / allProcessed) * 100).ToString());
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "2", processed2.CurrentNumberOfMarkers, (((Double)processed2.CurrentNumberOfMarkers / allProcessed) * 100).ToString());
            Console.WriteLine("{0,4}|{1,10}|{2,10}", "3", processed3.CurrentNumberOfMarkers, (((Double)processed3.CurrentNumberOfMarkers / allProcessed) * 100).ToString());
        }
    }
}
