<xsl:transform version="2.0"
               xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
               xmlns:dc="http://purl.org/dc/elements/1.1/"
               xmlns:content="http://purl.org/rss/1.0/modules/content/"
               xmlns:wp="http://wordpress.org/export/1.1/"
               xmlns:months="f:months">
<xsl:output method="xml" indent="yes" cdata-section-elements="post" />
<!-- this list is unfortunately needed because XSLT has no proper date formatting capabilities -->
<months:months>
  <name num="Jan">01</name>
  <name num="Feb">02</name>
  <name num="Mar">03</name>
  <name num="Apr">04</name>
  <name num="May">05</name>
  <name num="Jun">06</name>
  <name num="Jul">07</name>
  <name num="Aug">08</name>
  <name num="Sep">09</name>
  <name num="Oct">10</name>
  <name num="Nov">11</name>
  <name num="Dec">12</name>
</months:months>

<xsl:variable name="vMonths" select="document('')/*/months:*" />

<xsl:template match="/rss/channel">
	<xsl:element name="book">
		<xsl:for-each select='item[wp:post_type="post" and wp:status="publish"]'>
			<xsl:sort select="substring(pubDate, 13, 4)" data-type="number" order="ascending"/>
			<xsl:sort select="$vMonths/name[@num=substring(current()/pubDate, 9, 3)]" data-type="number" order="ascending"/>
			<xsl:sort select="substring(pubDate, 6, 2)" data-type="number" order="ascending"/>		
			<xsl:element name="post">
				<xsl:attribute name="title">
					<xsl:value-of select="title"/>
				</xsl:attribute>
				<xsl:attribute name="author">
					<xsl:value-of select="dc:creator"/>
				</xsl:attribute>
				<xsl:attribute name="date">
          <!-- build ISO 8601 combined date/time string -->
					<xsl:value-of select="substring(pubDate, 13, 4)"/>-<xsl:value-of select="$vMonths/name[@num=substring(current()/pubDate, 9, 3)]"/>-<xsl:value-of select="substring(pubDate, 6, 2)"/>T<xsl:value-of select="substring(pubDate, 17, 15)"/>
				</xsl:attribute>
				<!-- we want this in a CDATA section to prevent breaking later XML (same reason it is CDATA in the original!) -->
				<xsl:text disable-output-escaping="yes">&lt;![CDATA[</xsl:text> 
				<xsl:value-of select="content:encoded" disable-output-escaping="yes" />
				<xsl:text disable-output-escaping="yes">]]</xsl:text>
				<xsl:text disable-output-escaping="yes">&gt;</xsl:text>
			</xsl:element>
		</xsl:for-each>
	</xsl:element>
</xsl:template>
</xsl:transform>