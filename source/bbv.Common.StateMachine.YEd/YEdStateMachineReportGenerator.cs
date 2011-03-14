//-------------------------------------------------------------------------------
// <copyright file="YEdStateMachineReportGenerator.cs" company="bbv Software Services AG">
//   Copyright (c) 2008-2011 bbv Software Services AG
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace bbv.Common.StateMachine
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using bbv.Common.StateMachine.Internals;

    /// <summary>
    /// generates a graphml file that can be read by yEd.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class YEdStateMachineReportGenerator<TState, TEvent> : IStateMachineReport<TState, TEvent>
        where TState : struct, IComparable
        where TEvent : struct, IComparable
    {
        private static readonly XNamespace n = "http://graphml.graphdrawing.org/xmlns";
        private static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        private static readonly XNamespace y = "http://www.yworks.com/xml/graphml";
        private static readonly XNamespace yed = "http://www.yworks.com/xml/yed/3";
        private static readonly XNamespace schemaLocation = "http://graphml.graphdrawing.org/xmlns http://www.yworks.com/xml/schema/graphml/1.1/ygraphml.xsd";

        private readonly Stream outputStream;

        private int edgeId;

        private TState? initialStateId;

        /// <summary>
        /// Initializes a new instance of the <see cref="YEdStateMachineReportGenerator&lt;TState, TEvent&gt;"/> class.
        /// </summary>
        /// <param name="outputStream">The output stream.</param>
        public YEdStateMachineReportGenerator(Stream outputStream)
        {
            this.outputStream = outputStream;
        }

        /// <summary>
        /// Generates a report of the state machine.
        /// </summary>
        /// <param name="name">The name of the state machine.</param>
        /// <param name="states">The states.</param>
        /// <param name="initialStateId">The initial state id.</param>
        public void Report(string name, IEnumerable<IState<TState, TEvent>> states, TState? initialStateId)
        {
            this.edgeId = 0;

            this.initialStateId = initialStateId;
            Ensure.ArgumentNotNull(states, "states");

            var graph = new XElement(n + "graph", new XAttribute("edgedefault", "directed"), new XAttribute("id", "G"));

            this.AddNodes(graph, states);
            this.AddEdges(graph, states);
            
            var doc = new XDocument(
                new XElement(
                        n + "graphml",
                        new XComment("Created by Urs"),
                        new XElement(n + "key", new XAttribute("for", "graphml"), new XAttribute("id", "d0"), new XAttribute("yfiles.type", "resources")),
                        new XElement(n + "key", new XAttribute("for", "port"), new XAttribute("id", "d1"), new XAttribute("yfiles.type", "portgraphics")),
                        new XElement(n + "key", new XAttribute("for", "port"), new XAttribute("id", "d2"), new XAttribute("yfiles.type", "portgeometry")),
                        new XElement(n + "key", new XAttribute("for", "port"), new XAttribute("id", "d3"), new XAttribute("yfiles.type", "portuserdata")),
                        new XElement(n + "key", new XAttribute("attr.name", "url"), new XAttribute("attr.type", "string"), new XAttribute("for", "node"), new XAttribute("id", "d4")),
                        new XElement(n + "key", new XAttribute("attr.name", "description"), new XAttribute("attr.type", "string"), new XAttribute("for", "node"), new XAttribute("id", "d5")),
                        new XElement(n + "key", new XAttribute("for", "node"), new XAttribute("id", "d6"), new XAttribute("yfiles.type", "nodegraphics")),
                        new XElement(n + "key", new XAttribute("attr.name", "Beschreibung"), new XAttribute("attr.type", "string"), new XAttribute("for", "graph"), new XAttribute("id", "d7"), new XElement(n + "default")),
                        new XElement(n + "key", new XAttribute("attr.name", "url"), new XAttribute("attr.type", "string"), new XAttribute("for", "edge"), new XAttribute("id", "d8")),
                        new XElement(n + "key", new XAttribute("attr.name", "description"), new XAttribute("attr.type", "string"), new XAttribute("for", "edge"), new XAttribute("id", "d9")),
                        new XElement(n + "key", new XAttribute("for", "edge"), new XAttribute("id", "d10"), new XAttribute("yfiles.type", "edgegraphics")),
                        graph,    
                        new XElement(n + "data", new XAttribute("key", "d0"), new XElement(y + "Resources"))));

            doc.Root.SetAttributeValue(XNamespace.Xmlns + "y", y);
            doc.Root.SetAttributeValue(XNamespace.Xmlns + "xsi", xsi);
            doc.Root.SetAttributeValue(XNamespace.Xmlns + "yed", yed);
            doc.Root.SetAttributeValue(XNamespace.Xmlns + "schemaLocation", schemaLocation);

            doc.Save(this.outputStream);
        }

        private void AddEdges(XElement graph, IEnumerable<IState<TState, TEvent>> states)
        {
            foreach (var state in states)
            {
                foreach (var transition in state.Transitions.GetTransitions())
                {
                    this.AddEdge(graph, transition);
                }
            }
        }

        private void AddEdge(XElement graph, TransitionDictionary<TState, TEvent>.TransitionInfo transition)
        {
            var edge = new XElement(n + "edge", new XAttribute("id", transition.EventId + (this.edgeId++).ToString(CultureInfo.InvariantCulture)), new XAttribute("source", transition.Source.Id), new XAttribute("target", (transition.Target ?? transition.Source).Id));
            edge.Add(new XElement(n + "data", new XAttribute("key", "d10"), new XElement(y + "PolyLineEdge", new XElement(y + "Arrows", new XAttribute("source", "none"), new XAttribute("target", "standard")), new XElement(y + "EdgeLabel", (transition.HasGuard ? "[Guard]" : string.Empty) + transition.EventId))));

            graph.Add(edge);
        }

        private void AddNodes(XElement graph, IEnumerable<IState<TState, TEvent>> states)
        {
            foreach (var state in states.Where(s => s.SuperState == null))
            {
                this.AddNode(graph, state);
            }
        }

        private void AddNode(XElement graph, IState<TState, TEvent> state)
        {
            var node = new XElement(n + "node", new XAttribute("id", state.Id.ToString()));

            bool initialState = (this.initialStateId.HasValue && state.Id.ToString() == this.initialStateId.Value.ToString()) || (state.SuperState != null && state.SuperState.InitialState == state);

            if (state.SubStates.Any())
            {
                // alignment="right" autoSizePolicy="node_width" backgroundColor="#EBEBEB" modelName="internal" modelPosition="t"
                var label = new XElement(y + "NodeLabel", state.Id, new XAttribute("alignment", "right"), new XAttribute("autoSizePolicy", "node_width"), new XAttribute("backgroundColor", "#EBEBEB"), new XAttribute("modelName", "internal"), new XAttribute("modelPosition", "t"));

                var groupNode = new XElement(y + "GroupNode", label, new XElement(y + "State", new XAttribute("closed", "false"), new XAttribute("innerGraphDisplayEnabled", "true")));
                node.Add(new XElement(n + "data", new XAttribute("key", "d6"), new XElement(y + "ProxyAutoBoundsNode", new XElement(y + "Realizers", new XAttribute("active", "0"), groupNode))));

                if (initialState)
                {
                    groupNode.Add(new XElement(y + "BorderStyle", new XAttribute("width", "2.0")));
                }

                var subGraph = new XElement(n + "graph", new XAttribute("edgedefault", "directed"), new XAttribute("id", state.Id + ":"));
                node.Add(subGraph);
                foreach (var subState in state.SubStates)
                {
                    this.AddNode(subGraph, subState);
                }
            }
            else
            {
                var shape = new XElement(
                    y + "ShapeNode", 
                    new XElement(y + "NodeLabel", state.Id),
                    new XElement(y + "Shape", new XAttribute("type", "ellipse")));

                if (initialState)
                {
                    shape.Add(new XElement(y + "BorderStyle", new XAttribute("width", "2.0")));
                }

                node.Add(new XElement(n + "data", new XAttribute("key", "d6"), shape));
            }

            graph.Add(node);
        }
    }
}